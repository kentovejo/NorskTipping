using System;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Collections.Generic;
using Csla;
using Csla.Configuration;
using Csla.Rules;
using Library.Net;

namespace Security.Identity
{
    public struct UserEditSave
    {
        public string Error;
        public int FkProfile;
    }

    public class UserEdit
    {
        public UserEditSave Create(string userName, string email, string comment)
        {
            var clearPassword = UserPassword.GetClearPassword();
            // New user
            var u = User.NewUserEC(userName.Trim().ToLower());
            u.Password = string.Empty; // new SecureAuthentication().EncodePassword(u.UserName.Trim().ToLower() + clearPassword);
            // Store email in lower case
            u.Email = email.ToLower();
            u.Comment = comment;
            var error = Persist(u);
            if ((string.IsNullOrEmpty(ConfigurationManager.AppSettings["SetPasswordMethod"]) || ConfigurationManager.AppSettings["SetPasswordMethod"] == "ByMail") && !string.IsNullOrEmpty(u.Email) && string.IsNullOrEmpty(error.Error))
                error.Error = new UserEdit().SendEmailToUser(u.Email, $"Dette er en automatisk generert e-post. Det er ikke mulig å svare på denne e-posten.\r\n\r\nDitt brukernavn er {u.UserName} og passordet er {clearPassword}", "Ny brukerkonto");
            return error;
        }

        public string Update(string userName, string email, string comment, bool isActive)
        {
            var u = User.GetUserEC(userName);
            // Store email in lower case
            u.Email = email.ToLower();
            u.Comment = comment;
            u.IsActive = isActive;
            var error = Persist(u);
            return error.Error;
        }

        public string Update(string userName, string email, string comment, bool isActive, int failedPasswordCount, DateTime? lockoutEndDate, string securityStamp)
        {
            var u = User.GetUserEC(userName);
            // Store email in lower case
            u.Email = email.ToLower();
            u.Comment = comment;
            u.IsActive = isActive;
            u.FailedPasswordAttemptCount = failedPasswordCount;
            u.LockoutEndDateUtc = lockoutEndDate ?? DateTime.MinValue;
            u.SecurityStamp = securityStamp;
            var error = Persist(u);
            return error.Error;
        }

        public string Update(string userName, string email, string comment, bool isActive, string passwordHash, bool mustChangePassword, int failedPasswordCount, DateTime? lockoutEndDate, string securityStamp)
        {
            var u = User.GetUserEC(userName);
            // Store email in lower case
            u.Email = email.ToLower();
            u.Comment = comment;
            u.IsActive = isActive;
            u.Password = passwordHash;
            u.MustChangePassword = mustChangePassword;
            u.FailedPasswordAttemptCount = failedPasswordCount;
            u.LockoutEndDateUtc = lockoutEndDate ?? DateTime.MinValue;
            u.SecurityStamp = securityStamp;
            var error = Persist(u);
            return error.Error;
        }

        public UserEditSave Persist(User u)
        {
            var error = string.Empty;
            var fkUserProfile = 0;
            try
            {
                // Save user
                u = u.Save();
                fkUserProfile = u.FkUserprofile;
            }
            catch (Exception err)
            {
                error = err.Message;
            }

            return new UserEditSave { Error = error, FkProfile = fkUserProfile };
        }

        public int CreateNewPerson(string personName, string lastName)
        {
            //var personBO = PersonEC.NewPersonEC();
            //personBO.FirstName = personName;
            //personBO.LastName = lastName;
            //// Save person
            //var genericBO = CoreTypes.GenericSingleObjectSaver.SaveObject(personBO);
            //var error = genericBO.ErrorText;
            //PersonEC savedPerson = null;
            //if (string.IsNullOrEmpty(error))
            //    savedPerson = (PersonEC)genericBO.Result;
            //if (string.IsNullOrEmpty(error))
            //    if (savedPerson == null)
            //    {
            //        error = "Kunne ikke opprette ny person.";
            //    }

            //if (!string.IsNullOrEmpty(error))
            //    throw new Exception(error);
            //return savedPerson?.Id ?? 0;
            return 0;
        }

        public string Unlock(string userName)
        {
            return Unlock(userName, DateTime.UtcNow.AddMinutes(-5));
        }

        public string Unlock(string userName, DateTime unlockDate)
        {
            var u = User.GetUserEC(userName);
            // Unlock user
            u.LockoutEndDateUtc = unlockDate;
            u.FailedPasswordAttemptCount = 0;
            u = u.Save();
            // Save user
            return string.Empty;
        }

        /// <summary>
        ///     Save user roles to persistent storage
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="values"></param>
        /// <returns>Error message, if any</returns>
        public string SaveUserRole(string userName, List<object> values)
        {
            var error = string.Empty;
            var userRoleList = UserRoleCollection.GetUserRoleERList(userName);
            // Update user roles.
            foreach (var item in userRoleList)
            {
                var contains = values.Any(p => (string)p == item.FkRole);
                item.Selected = contains;
            }

            try
            {
                userRoleList = userRoleList.Save();
            }
            catch (ValidationException ex)
            {
                var message = new StringBuilder();
                message.AppendFormat("{0}<br/>", ex.Message);
                error = message.ToString();
            }
            catch (DataPortalException ex)
            {
                error = ex.BusinessException is SqlException ? ex.Message : $"Ukjent feil:\r\n{ex.BusinessException.Message}";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

        public string SendEmailToUser(string email, string messageBody, string messageSubject)
        {
            var error = string.Empty;
            // SMTP email address (from)
            var emailSmtp = "noreply@abc.com";
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SMTPMailFrom"]))
                emailSmtp = ConfigurationManager.AppSettings["SMTPMailFrom"];
            // Send email to user
            using (var client = new SmtpClient())
            {
                var from = new MailAddress(emailSmtp, "CurIT", Encoding.UTF8);
                // Set destinations for the e-mail message.
                var to = new MailAddress(email);
                // Specify the message content.
                var message = new MailMessage(from, to)
                {
                    Body = messageBody,
                    BodyEncoding = Encoding.UTF8,
                    Subject = "CurIT - " + messageSubject,
                    SubjectEncoding = Encoding.UTF8
                };
                try
                {
                    client.Send(message);
                }
                catch (Exception sendError)
                {
                    error = sendError.Message;
                }
                finally
                {
                    // Clean up.
                    message.Dispose();
                }
            }

            return error;
        }
    }
}
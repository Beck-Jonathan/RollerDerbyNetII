using System.Net.Mail;

namespace DataObjects
{
    // this class will hold extention helpers
    // they must be in a public static class
    public static class ValidationHelpers
    {
        // the methods must be public static, and include
        // "this" as the first parameter
        public static bool IsValidDerbyName(this string derbyName)
        {
            bool isValid = false;
            if (
                 derbyName.Length >= 4
                && derbyName.Length <= 100)
            {
                isValid = true;
            }



            return isValid;
        }
        public static bool isValidPassword(this string password)
        {
            bool isValid = false;
            // this needs to be done right eventually
            if (password.Length >= 7)
            {
                isValid = true;

            }


            return isValid;

        }
        public static bool isValidNVarChar100(this string _string)
        {
            bool isValid = false;
            if (_string.Length <= 100 && _string.Length > 3)
            {

                isValid = true;
            }

            return isValid;

        }

        public static bool isValidzip(this string _string)
        {
            bool isValid = false;
            if (_string.Length == 5)
            {

                isValid = true;
            }

            return isValid;

        }
        public static bool isValidPhone(this string _string)
        {
            bool isValid = false;
            if (_string.Length == 13)
            {

                isValid = true;
            }

            return isValid;

        }
        public static bool isValidNVarChar50(this string _string)
        {
            bool isValid = false;
            if (_string.Length <= 50 && _string.Length > 3)
            {

                isValid = true;
            }

            return isValid;

        }

        public static bool isValidDecimal(this string _decimal)
        {
            bool isValid = false;
            decimal parsedDecimal = 0.0m;
            if (isValid = decimal.TryParse(_decimal, out parsedDecimal))
            {

                isValid = true;
            }

            return isValid;

        }
        public static bool isValidEmail(this string _email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(_email);
            }
            catch
            {
                valid = false;
            }
            if (_email.Length < 8 && _email.Length > 250)
            {
                valid = false;
            }

            return valid;


        }
    }
}

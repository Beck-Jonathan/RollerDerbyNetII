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
    }
}

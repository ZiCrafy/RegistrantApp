namespace RegistrantApp.Shared.Validators;

public class MyValidator
{
    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes); // .NET 5 +
            // Convert the byte array to hexadecimal string prior to .NET 5
            // StringBuilder sb = new System.Text.StringBuilder();
            // for (int i = 0; i < hashBytes.Length; i++)
            // {
            //     sb.Append(hashBytes[i].ToString("X2"));
            // }
            // return sb.ToString();
        }
    }
    
    public static long PhoneMinimalValidation(string number)
    {
        number = number.ToUpper();
        if (string.IsNullOrEmpty(number))
            return Convert.ToInt64(number);

        number = number.Replace(" ", string.Empty)
            .Replace("+", string.Empty)
            .Replace("(", string.Empty)
            .Replace(")", string.Empty)
            .Replace("-", string.Empty);

        while (number[0].ToString() == "8" || number[0].ToString() == "7")
        {
            number = number.Substring(1);
        }

        return Convert.ToInt64(number);
    }
}
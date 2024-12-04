/*
 * Program Author: Reham Afzal  
 * ID: w10171356   
 * Assignment: Password Manager part 2
 */

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Username: {UserName}";
        }
    }
}

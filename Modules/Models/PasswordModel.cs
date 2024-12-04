/*
 * Program Author:  Rehm Afzal  
 * ID: w10171356  
 * Assignment: Password Manager part 2
 * 
 * Description:
 * Implements the model for storing password information.
 */

using System.ComponentModel.DataAnnotations;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class PasswordModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string PlatformName { get; set; }

        [Required, MaxLength(100)]
        public string PlatformUserName { get; set; }

        /// <summary>
        /// The encrypted password, stored as a byte array.
        /// </summary>
        [Required]
        public byte[] PasswordText { get; set; }

        /// <summary>
        /// Returns a human-readable representation of the password model, excluding sensitive data.
        /// </summary>
        public override string ToString()
        {
            return $"Platform: {PlatformName}, Username: {PlatformUserName}";
        }
    }
}

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
        /// <summary>
        /// Unique identifier for the password entry.
        /// </summary>
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// The ID of the user who owns this password.
        /// </summary>
        [Required]
        public int UserID { get; set; }

        /// <summary>
        /// The name of the platform (e.g., "Google", "Facebook").
        /// </summary>
        [Required, MaxLength(100)]
        public string PlatformName { get; set; }

        /// <summary>
        /// The username used for the specified platform.
        /// </summary>
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

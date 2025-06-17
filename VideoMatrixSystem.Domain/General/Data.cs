namespace VideoMatrixSystem.Domain.General
{
    public static class Data
    {
        #region NORMALIZATION OF IDENTIFIERS

        /// <summary> Returns the name from an identifier
        /// </summary>
        /// <param name="cIden"> Full expression of the identifier </param>
        /// <returns> Base name contained in the identifier </returns>
        public static string GetName(string cIden)
        {
            string cName = null;

            if (cIden != null)
            {
                int nIndex = cIden.IndexOf('_');

                if (nIndex >= 0)
                    cName = cIden[(nIndex + 1)..];
                else
                    cName = cIden;
            }

            return cName;
        }

        /// <summary> Returns the table name from an identifier </summary>
        /// <param name="cIden"> Full expression of the identifier </param>
        /// <returns> Table name contained in the identifier </returns>
        public static string GetTable(string cIden)
        {
            string cTable = null;

            if (cIden != null)
            {
                int nIndex = cIden.IndexOf('_');

                if (nIndex >= 0)
                    cTable = cIden[..nIndex];
                else
                    cTable = string.Empty;
            }

            return cTable;
        }
        #endregion

        #region ENUM UTILITIES

        /// <summary>
        /// Determines whether the specified key is defined in the enumeration type.
        /// </summary>
        /// <typeparam name="T">The enumeration type to check against.</typeparam>
        /// <param name="key">The string representation of the enum value.</param>
        /// <returns>True if the key is defined in the enum; otherwise, false.</returns>
        public static bool IsDefined<T>(string key) where T : Enum
        {
            if (string.IsNullOrEmpty(key))
                return false;

            return Enum.IsDefined(typeof(T), key);
        }

        /// <summary>
        /// Converts an enum value to its associated code.
        /// It finds valid values ignoring uppercase and lowercase differences.
        /// However, it is faster if the exact enum name is provided.
        /// If a string with digits is given, it assumes it is a specific value.
        /// </summary>
        /// <typeparam name="T"> The enum type to convert </typeparam>
        /// <param name="value"> The value to search and convert </param>
        /// <param name="defval"> The default value to assign </param>
        /// <returns> The ID of the found enum or null </returns>
        public static T GetEnum<T>(string value, T defval) where T : Enum
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (char.IsDigit(value[0]))
                    return (T)(object)Convert.ToInt32(value);
                else
                {
                    if (Enum.IsDefined(typeof(T), value))
                        return (T)Enum.Parse(typeof(T), value);
                    else
                    {
                        foreach (string name in Enum.GetNames(typeof(T)))
                        {
                            if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                                return (T)Enum.Parse(typeof(T), value, true);
                        }
                    }
                }
            }

            return defval;
        }

        /// <summary>
        /// Retrieves the enum value corresponding to the given string.
        /// If the value is not found, the default value for the enum is returned.
        /// </summary>
        /// <typeparam name="T">The enum type to convert to.</typeparam>
        /// <param name="value">The string representation of the enum value.</param>
        /// <returns>The matching enum value, or the default if not found.</returns>
        public static T GetEnum<T>(string value) where T : Enum
        {
            return GetEnum<T>(value, default);
        }

        #endregion

    }
}

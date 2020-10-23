﻿/*
 * MIT License
 * 
 * Copyright (c) 2020 Stanislaw Schlosser <http://github.com/0x2aff>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace ADNCWebpackBoilerplate
{

    /// <summary>
    ///     Application configuration.
    /// </summary>
    public class Config
    {
        #region Database Settings

        /// <summary>
        ///     Database host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     Database port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Database username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Database password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Database name.
        /// </summary>
        public string Database { get; set; }

        #endregion
    }
}
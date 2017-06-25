/* Этот файл является частью библиотеки Saraff.Twain.Extensions
 * © SARAFF SOFTWARE (Кирножицкий Андрей), 2017.
 * Saraff.Twain.Extensions - свободная программа: вы можете перераспространять ее и/или
 * изменять ее на условиях Меньшей Стандартной общественной лицензии GNU в том виде,
 * в каком она была опубликована Фондом свободного программного обеспечения;
 * либо версии 3 лицензии, либо (по вашему выбору) любой более поздней
 * версии.
 * Saraff.Twain.Extensions распространяется в надежде, что она будет полезной,
 * но БЕЗО ВСЯКИХ ГАРАНТИЙ; даже без неявной гарантии ТОВАРНОГО ВИДА
 * или ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. Подробнее см. в Меньшей Стандартной
 * общественной лицензии GNU.
 * Вы должны были получить копию Меньшей Стандартной общественной лицензии GNU
 * вместе с этой программой. Если это не так, см.
 * <http://www.gnu.org/licenses/>.)
 * 
 * This file is part of Saraff.Twain.Extensions.
 * © SARAFF SOFTWARE (Kirnazhytski Andrei), 2017.
 * Saraff.Twain.Extensions is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * Saraff.Twain.Extensions is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * You should have received a copy of the GNU Lesser General Public License
 * along with Saraff.Twain.Extensions. If not, see <http://www.gnu.org/licenses/>.
 * 
 * PLEASE SEND EMAIL TO:  twain@saraff.ru.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saraff.Twain.Extensions {

    public static class XCapabilityExtension {

        /// <summary>
        /// Sets value of a capability.
        /// </summary>
        /// <typeparam name="T">Type of a capability.</typeparam>
        /// <param name="capability">The capability.</param>
        /// <param name="callback">The callback function.</param>
        /// <returns>Instance of a capability.</returns>
        public static XCapability<T> Set<T>(this XCapability<T> capability,Func<XCapability<T>,XCapabilityValue<T>> callback) {
            callback(capability)?.Set();
            return capability;
        }

        /// <summary>
        /// Sets value of a capability.
        /// </summary>
        /// <typeparam name="T">Type of a capability.</typeparam>
        /// <param name="capability">The capability.</param>
        /// <param name="callback">The callback function.</param>
        /// <returns>Instance of a capability.</returns>
        public static XCapability<T> Set<T>(this XCapability<T> capability,Func<XCapability<T>,T> callback) {
            capability.Set(callback(capability));
            return capability;
        }
    }
}

﻿using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.Base
{
    public class SimplestСomponent : ISimplestComponent
    {
        public int Id { get; set; }

        /// <summary>
        /// Модель компонента ("С2000-СП1 исп.01 АЦДР.425412.001-01")
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Тип прибора ("Блок сигнально-пусковой")
        /// </summary>
        public string DeviceType { get; set; } = string.Empty;

        /// <summary>
        /// Обозначение компонента на схеме ("SR1.3")
        /// </summary>
        public string Designation { get; set; } = string.Empty;

        /// <summary>
        /// Обозначение шкафа в котором находится этот дивайс
        /// </summary>
        public string ParentName { get; set; } = string.Empty;

        public bool Equals(SimplestСomponent obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (GetType() != obj.GetType())
                return false;
            return string.Compare(this.Designation, obj.Designation, StringComparison.CurrentCulture) == 0 &&
                   string.Compare(this.Model, obj.Model, StringComparison.CurrentCulture) == 0 &&
                   string.Compare(this.DeviceType, obj.DeviceType, StringComparison.CurrentCulture) == 0;
        }

        public override int GetHashCode()
        {
            var hashCode = 1485357867;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DeviceType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DeviceType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Designation);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Designation);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParentName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParentName);
            return hashCode;
        }
    }
}

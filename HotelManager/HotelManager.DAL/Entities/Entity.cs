using System;
using System.ComponentModel.DataAnnotations;
using HotelManager.DAL.Interfaces;

namespace HotelManager.DAL.Entities
{
    public abstract class Entity : IEntity
    {
        [Required]
        public int Id { get; set; }
    }
}

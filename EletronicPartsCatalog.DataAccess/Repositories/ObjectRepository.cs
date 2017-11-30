﻿using System;
using System.Collections.Generic;
using System.Linq;
using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.Contracts.Repositories;
using Object = EletronicPartsCatalog.DataAccess.Models.Object;

namespace EletronicPartsCatalog.DataAccess.Repositories
{
    public class ObjectsRepository : IObjectsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ObjectsRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }


        public List<ObjectDto> GetAll() {
            return _dbContext.Objects
                .Where(x => x.IsDeleted == false)
                .Select(x => new ObjectDto() {
                    Id = x.Id,
                    Name = x.Name,
                    ObjectParts = x.ObjectParts,
                    Description = x.Description,
                    IsDeleted = x.IsDeleted,
                    CreationDate = x.CreationDate,
                    CreatedBy = x.CreatedBy
                })
                .ToList();
        }

        public ObjectDto GetById(int id) {
            var obj = _dbContext.Objects.First(x => x.Id == id);
            var objparts = new List<ObjectPartDto>();

            foreach (var o in _dbContext.ObjectParts)
            {
                if(o.ObjectId == obj.Id)
                    objparts.Add(o);
            }
            return new ObjectDto {
                Id = obj.Id,
                Name = obj.Name,
                ObjectParts = objparts,
                Description = obj.Description,
                CreationDate = obj.CreationDate,
                IsDeleted = obj.IsDeleted,
                CreatedBy = obj.CreatedBy,
            };
        }

        public void Add(AddObjectDto obj) {
            _dbContext.Objects.Add(new Object {
                Name = obj.Name,
                Description = obj.Description,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                CreatedBy = obj.CreatedBy,
                ObjectParts = obj.ObjectParts
            });

            _dbContext.SaveChanges();
        }

        public ObjectDto GetByName(string objectName) {
            var existingObject = _dbContext.Objects.FirstOrDefault(x => x.Name.ToLower() == objectName.ToLower());

            if (existingObject != null) {
                return new ObjectDto {
                    Id = existingObject.Id,
                    Name = existingObject.Name,
                    ObjectParts = existingObject.ObjectParts,
                    Description = existingObject.Description,
                    CreationDate = existingObject.CreationDate,
                    IsDeleted = existingObject.IsDeleted,
                    CreatedBy = existingObject.CreatedBy
                };
            }

            return null;
        }

        public void Delete(int id) {
            var obj = _dbContext.Objects.First(x => x.Id == id);
            obj.IsDeleted = true;

            _dbContext.SaveChanges();
        }
    }

}



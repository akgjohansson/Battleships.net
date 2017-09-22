using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;



namespace Battleships.net.DataBase.Builder
{
    public class AutoMapper
    {
        private readonly ModelMapper _modelMapper;
        public AutoMapper()
        {
            _modelMapper = new ModelMapper();
        }
        public HbmMapping Map()
        {
            MapGame();
            MapPlayer();
            MapUser();
            MapShip();
            MapGrid();
            return _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
        }
        private void MapGame()
        {
            _modelMapper.Class<Game>(e =>
            {
                e.Id(p => p.GameId, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.StartedAt, p => p.NotNullable(false));
                e.Property(p => p.Rows);
                e.Property(p => p.Columns);
                e.Set(p => p.Players, p =>
                {
                    p.Cascade(Cascade.All);
                    p.Inverse(true);
                    p.Key(k => k.Column(col => col.Name("GameId")));
                } , p => p.OneToMany());
            });
        }
        private void MapPlayer()
        {
            _modelMapper.Class<Player>(e =>
            {
                e.Id(p => p.PlayerId, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.IsHost);
                e.Set(p => p.Ship, p =>
                {
                    p.Inverse(true);
                    p.Cascade(Cascade.All);
                    p.Key(k => k.Column(col => col.Name("PlayerId")));
                }, p => p.OneToMany());
                e.ManyToOne(p => p.Game, mapper =>
               {
                   mapper.Column("GameId");
                   mapper.NotNullable(true);
                   mapper.Cascade(Cascade.None);
               });
                e.Set(p => p.Grid, p =>
                  {
                      p.Inverse(true);
                      p.Cascade(Cascade.All);
                      p.Key(k => k.Column(col => col.Name("PlayerId")));
                  }, p => p.OneToMany());
                e.ManyToOne(p => p.UserPerson, mapper =>
                {
                    mapper.Column("UserPersonId");
                    mapper.NotNullable(true);
                    mapper.Cascade(Cascade.None);
                });
            });
        }
        private void MapShip()
        {
            _modelMapper.Class<Ship>(e =>
            {
                e.Id(p => p.ShipId, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.StartGrid);
                e.Property(p => p.Orientation);
                e.Property(p => p.Length);
                e.Property(p => p.IsSunk);
                e.ManyToOne(p => p.Player, mapper =>
               {
                   mapper.Column("PlayerId");
                   mapper.NotNullable(true);
                   mapper.Cascade(Cascade.None);
               });
                e.Set(p => p.Grid, p =>
                {
                    p.Inverse(true);
                    p.Cascade(Cascade.All);
                    p.Key(k => k.Column(col => col.Name("ShipId")));
                }, p => p.OneToMany());

            });
        }
        private void MapUser()
        {
            _modelMapper.Class<UserPerson>(e =>
            {
              
                e.Id(p => p.UserPersonId, p =>
                {
                    p.Generator(Generators.GuidComb);
                   
                });
                e.Property(p => p.NickName);
                e.Set(p => p.Player, p =>
               {
                   p.Inverse(true);
                   p.Cascade(Cascade.All);
                   p.Key(k => k.Column(col => col.Name("UserPersonId")));
               }, p => p.OneToMany());
            });
        }
        private void MapGrid()
        {
            _modelMapper.Class<Grid>(e =>
            {
                e.Id(p => p.GridId, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Coordinate);
                e.Property(p => p.IsHit);
                e.ManyToOne(p => p.Ship, mapper =>
                {
                    mapper.Column("ShipId");
                    mapper.NotNullable(false);
                    mapper.Cascade(Cascade.None);
                });
                e.ManyToOne(p => p.Player, mapper =>
                 {
                     mapper.Column("PlayerId");
                     mapper.NotNullable(true);
                     mapper.Cascade(Cascade.None);
                 });
            });
        }
    }
};
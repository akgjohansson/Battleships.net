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
            return _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
        }
        private void MapGame()
        {
            _modelMapper.Class<Game>(e =>
            {
                e.Id(p => p.GameId, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.StartedAt);
                e.Set(p => p.Players, p =>
                {
                    p.Inverse(true);
                    p.Cascade(Cascade.All);
                    p.Key(k => k.Column(col => col.Name("GameId")));
                }, p => p.OneToMany());
            });
        }
        private void MapPlayer()
        {
            _modelMapper.Class<Player>(e =>
            {
                e.Id(p => p.PlayerId, p => p.Generator(Generators.GuidComb));
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
                e.ManyToOne(p => p.User, mapper =>
                {
                    mapper.Column("UserId");
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
            });
        }
        private void MapUser()
        {
            _modelMapper.Class<User>(e =>
            {
                e.Id(p => p.UserId, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.NickName);
                e.Table("[User]");
                e.Set(p => p.Player, p =>
               {
                   p.Inverse(true);
                   p.Cascade(Cascade.All);
                   p.Key(k => k.Column(col => col.Name("UserId")));
               }, p => p.OneToMany());
            });
        }
    }
};
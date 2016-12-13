using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RogueSharp;
using RpgGame.Data.Interfaces;
using RpgGame.ModelDTOs;
using RpgGame.ModelDTOs.Map;
using RpgGame.ModelDTOs.Monsters;
using RpgGame.Models;

namespace RpgGame.Data
{
    public class MappingService
    {
        private readonly IUnitOfWork unit;

        public MappingService(IUnitOfWork unit)
        {
            this.MapObjects();
            this.unit = unit;
        }

        public IUnitOfWork UnitOfWork => this.unit;

        public DungeonMap GetDungeonById(int id)
        {
            return this.unit.Dungeons.GetById(id);
        }

        public DungeonMapDTO GetDungeonDtoById(int id)
        {
            var dungeonDto = Mapper.Map<DungeonMapDTO>(this.GetDungeonById(id));
            dungeonDto.Initialize();
            return dungeonDto;
        }

        public IEnumerable<Monster> GetAllMonsters()
        {
            return this.unit.Monsters.GetAll();
        }

        public IEnumerable<MonsterDTO> GetAllMonsterDtos()
        {
            return this.GetAllMonsters().Select(Mapper.Map<MonsterDTO>);
        }

        public Monster GetMonsterByPosition(int x, int y)
       {
            return this.unit.Monsters.GetFirst(m => m.X == x && m.Y == y);
        }

        public Player GetPlayerById(int id)
        {
            return this.unit.Players.GetById(id);
        }

        public PlayerDTO GetPlayerDtoById(int id)
        {
            return Mapper.Map<PlayerDTO>(this.GetPlayerById(id));
        }

        public void AddPlayer(Player player)
        {
            this.unit.Players.Add(player);

            this.unit.Commit();
        }

        public void AddDungeon(DungeonMap map)
        {
            this.unit.Dungeons.Add(map);

            this.unit.Commit();
        }

        public void AddRoomsToDungeon(IEnumerable<Room> rooms, int dungeonId)
        {
            foreach (var room in rooms)
            {
                this.GetDungeonById(dungeonId).Rooms.Add(room);
            }

            this.unit.Commit();
        }

        public void AddTunelToDungeon(Tunel tunel, int dungeonId)
        {
            this.GetDungeonById(dungeonId).Tunels.Add(tunel);

            this.unit.Commit();
        }

        public void AddMonster(Monster monster)
        {
            this.unit.Monsters.Add(monster);

            this.unit.Commit();
        }

        public void AddMonsters(IEnumerable<Monster> monsters)
        {
            this.unit.Monsters.AddRange(monsters);

            this.unit.Commit();
        }

        private void MapObjects()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<Door, DoorDTO>();

                c.CreateMap<Tunel, TunelDTO>();

                c.CreateMap<Room, RoomDTO>()
                .ForMember(r => r.DungeonRoom,
                    m => m.MapFrom(dto => new Rectangle(dto.X, dto.Y, dto.Width, dto.Height)));

                c.CreateMap<Player, PlayerDTO>();

                c.CreateMap<Monster, MonsterDTO>().Include<Orc,OrcDTO>();
                c.CreateMap<Orc, OrcDTO>();

                c.CreateMap<DungeonMap, DungeonMapDTO>();
            });
        }
    }
}
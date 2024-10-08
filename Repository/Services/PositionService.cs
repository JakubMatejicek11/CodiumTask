using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class PositionService
    {
        private readonly DataContext _dataContext;

        public PositionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Position>> GetPositionsAsync()
        {
            return await _dataContext.Positions.ToListAsync();
        }
        public async Task<Position> GetPositionAsync(int id)
        {
            Position? position = await _dataContext.Positions.FindAsync(id);
            if (position == null)
            {
                return null;
            }

            return position;
        }

        public async Task UploadPositionsAsync(List<string> positionNames)
        {
            HashSet<string> uniquePositionNames = new HashSet<string>();

            foreach (string positionName in positionNames)
            {
                if (string.IsNullOrEmpty(positionName))
                {
                    continue;
                }

                if (uniquePositionNames.Contains(positionName))
                {
                    continue;
                }

                uniquePositionNames.Add(positionName);

                if (_dataContext.Positions.Any(p => p.PositionName == positionName))
                {
                    continue;
                }

                Position position = new Position
                {
                    PositionName = positionName
                };

                _dataContext.Positions.Add(position);
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}

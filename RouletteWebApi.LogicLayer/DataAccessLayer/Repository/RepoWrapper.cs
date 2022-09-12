using RouletteWebApi.LogicLayer.DataAccessLayer.Interfaces;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.DataAccessLayer.Repository
{
    public class RepoWrapper : IRepoWrapper
    {

        private IDbContext _context;
        private Payout _payout;
        private PlaceBet _placeBet;
        private Spin _spin;
        private User _user;


        public RepoWrapper(IDbContext context)
        {
            _context = context;
        }

        public IPayout Payout
        {
         get { 
                if(_payout == null) 
                { 
                    _payout = new Payout(_context);
                } 
                return _payout; 
            }
        }

        public IPlaceBet PlaceBet
        {
            get
            {
                if (_placeBet == null)
                {
                    _placeBet = new PlaceBet(_context);
                }
                return (_placeBet);
            }
        }

        public ISpin Spin
        {
            get
            {
                if (_spin == null)
                {
                    _spin = new Spin(_context);
                }
                return (_spin);
            }
        }

        public Iuser User
        {
            get
            {
                if (_user == null)
                {
                    _user = new User(_context);
                }
                return (_user);
            }
        }
    }
}

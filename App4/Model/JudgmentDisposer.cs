using System;

namespace 点棒数え.Model
{
    internal class JudgmentDisposer : IDisposable
    {
        private Player player;

        public JudgmentDisposer(Player player)
        {
            this.player = player;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
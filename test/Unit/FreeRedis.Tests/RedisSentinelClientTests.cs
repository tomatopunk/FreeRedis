using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FreeRedis.Tests.RedisSentinelClientTests
{
    public class SentinelTests
    {
        public static RedisSentinelClient GetClient() => new RedisSentinelClient("127.0.0.1:6379");

        [Fact(Skip = "test")]
        public void Ping()
        {
            using (var cli = GetClient())
            {
                Assert.Equal("PONG", cli.Ping());
            }
        }

        [Fact(Skip = "test")]
        public void Info()
        {
            using (var cli = GetClient())
            {
                var rt = cli.Info();
            }
        }

        [Fact(Skip = "test")]
        public void Role()
        {
            using (var cli = GetClient())
            {
                var rt = cli.Role();
                Assert.Equal(RoleType.Sentinel, rt.role);
                Assert.True(rt.masters.Any());
                Assert.Equal("mymaster", rt.masters.FirstOrDefault());
            }
        }

        [Fact(Skip = "test")]
        public void Masters()
        {
            using (var cli = GetClient())
            {
                var rt = cli.Masters();
                Assert.True(rt.Any());
                Assert.Equal("mymaster", rt[0].name);
            }
        }

        [Fact(Skip = "test")]
        public void Master()
        {
            using (var cli = GetClient())
            {
                var rt = cli.Master("mymaster");
                Assert.NotNull(rt);
                Assert.Equal("mymaster", rt.name);

                Assert.Equal("ERR No such master with that name",
                    Assert.Throws<RedisServerException>(() => cli.Master("mymaster222")).Message);
            }
        }

        [Fact(Skip = "test")]
        public void Salves()
        {
            using (var cli = GetClient())
            {
                var rt = cli.Salves("mymaster");
                Assert.True(rt.Any());
                Assert.Equal("ok", rt[0].master_link_status);
            }
        }

        [Fact(Skip = "test")]
        public void Sentinels()
        {
            using (var cli = GetClient())
            {
                var rt = cli.Sentinels("mymaster");
                Assert.True(rt.Any());
                Assert.Equal("127.0.0.1", rt[0].ip);
            }
        }

        [Fact(Skip = "test")]
        public void GetMasterAddrByName()
        {
            using (var cli = GetClient())
            {
                var rt = cli.GetMasterAddrByName("mymaster");
                Assert.False(string.IsNullOrEmpty(rt));
            }
        }

        [Fact(Skip = "test")]
        public void IsMasterDownByAddr()
        {
            using (var cli = GetClient())
            {
                var st = cli.Sentinels("mymaster");
                Assert.True(st.Any());
                var rt = cli.IsMasterDownByAddr(st[0].name, st[0].port, st[0].voted_leader_epoch, st[0].runid);
                Assert.NotNull(rt);
                Assert.False(rt.down_state);
                Assert.Equal("*", rt.leader);
                Assert.Equal(st[0].voted_leader_epoch, rt.vote_epoch);
            }
        }

        [Fact(Skip = "test")]
        public void Reset()
        {
            using (var cli = GetClient())
            {
                var rt = cli.Reset("*");
                Assert.True(rt > 0);
            }
        }

        [Fact(Skip = "test")]
        public void Failover()
        {
            using (var cli = GetClient())
            {
                cli.Failover("mymaster");

                Assert.Equal("ERR No such master with that name",
                    Assert.Throws<RedisServerException>(() => cli.Failover("mymaster222")).Message);
            }
        }

        [Fact(Skip = "test")]
        public void PendingScripts()
        {
            using (var cli = GetClient())
            {
                var rt = cli.PendingScripts();
            }
        }

        [Fact(Skip = "test")]
        public void FlushConfig()
        {
            using (var cli = GetClient())
            {
                cli.FlushConfig();
            }
        }

        [Fact(Skip = "test")]
        public void Remove()
        {
            using (var cli = GetClient())
            {
                //cli.Remove("mymaster");

                Assert.Equal("ERR No such master with that name",
                    Assert.Throws<RedisServerException>(() => cli.Remove("mymaster222")).Message);
            }
        }

        [Fact(Skip = "test")]
        public void CkQuorum()
        {
            using (var cli = GetClient())
            {
                var rt = cli.CkQuorum("mymaster");

                Assert.Equal("ERR No such master with that name",
                    Assert.Throws<RedisServerException>(() => cli.CkQuorum("mymaster222")).Message);
            }
        }

        [Fact(Skip = "test")]
        public void Set()
        {
            using (var cli = GetClient())
            {
                cli.Set("mymaster", "down-after-milliseconds", "5000");
            }
        }

        [Fact(Skip = "test")]
        public void InfoCache()
        {
            using (var cli = GetClient())
            {
                var rt = cli.InfoCache("mymaster");
            }
        }

        [Fact(Skip = "test")]
        public void SimulateFailure()
        {
            using (var cli = GetClient())
            {
                cli.SimulateFailure(true, true);
            }
        }
    }
}
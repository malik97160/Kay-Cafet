using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.CodeConfiguration
{
    public class RefreshTokenCodeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(b => b.Token).HasName("PK_refreshTokens_Token");
            builder.Property(b => b.Token).ValueGeneratedOnAdd();
            builder.HasOne(r => r.User).WithOne(u => u.RefreshToken).HasForeignKey<User>(r => r.RefreshTokenId);
        }
    }
}

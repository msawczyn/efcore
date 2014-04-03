// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Migrations.Model;
using Microsoft.Data.Relational.Model;
using Moq;
using Xunit;

namespace Microsoft.Data.Migrations.Tests.Model
{
    public class AddColumnOperationTest
    {
        [Fact]
        public void Create_and_initialize_operation()
        {
            var column = new Column("Foo", "int");
            var addColumnOperation = new AddColumnOperation("dbo.MyTable", column);

            Assert.Equal("dbo.MyTable", addColumnOperation.TableName);
            Assert.Same(column, addColumnOperation.Column);
            Assert.False(addColumnOperation.IsDestructiveChange);
        }

        [Fact]
        public void Dispatches_visitor()
        {
            var addColumnOperation = new AddColumnOperation("dbo.MyTable", new Column("Foo", "int"));
            var mockVisitor = new Mock<MigrationOperationSqlGenerator>();
            var builder = new Mock<IndentedStringBuilder>();
            addColumnOperation.GenerateSql(mockVisitor.Object, builder.Object, false);

            mockVisitor.Verify(g => g.Generate(addColumnOperation, builder.Object, false), Times.Once());
        }
    }
}

1.dotnet ef migrations add  生成一条迁移

2.dotnet ef migrations remove  删除最新一次迁移

3. dotnet ef  database  update 生成迁移到数据库，跟上面pmc命令类似 后面加指定的迁移作为参数可以进行版本的回滚

4.dotnet ef migrations script   也跟pmc类似  如果没有任何参数的话默认是生成所有sql脚本，但是参数格式略有不同如下：dotnet ef migrations script migrationName1  migrationName2 ; 是像这样直接跟迁移名称的也就是生成migrationName1 到migrationName2 的sql脚本


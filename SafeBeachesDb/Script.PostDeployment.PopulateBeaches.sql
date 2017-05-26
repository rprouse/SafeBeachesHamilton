/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/
INSERT INTO dbo.Beach Values (1, 'Bayfront Park Beach', '200 Harbour Front Drive, Hamilton', 'POINT(43.271584 -79.874634)');
INSERT INTO dbo.Beach Values (2, 'Beach Boulevard', 'Beach Boulevard, Hamilton', 'POINT(43.277482 -79.780652)');
INSERT INTO dbo.Beach Values (3, 'Binbrook Conservation Area Beach', '5050 Harrison Road, Binbrook', 'POINT(43.101739 -79.833129)');
INSERT INTO dbo.Beach Values (4, 'Christie Conservation Area Beach', '1000 Highway 5 West, Dundas', 'POINT(43.280835 -80.022795)');
INSERT INTO dbo.Beach Values (5, 'Confederation Park Beach', '680 Van Wagner''s Beach Road, Hamilton', 'POINT(43.250981 -79.754903)');
INSERT INTO dbo.Beach Values (6, 'Pier 4 Park Beach', 'Bay Street North at Leander Drive, Hamilton', 'POINT(43.273646 -79.867682)');
INSERT INTO dbo.Beach Values (7, 'Valens Conservation Area Beach', '1691 Regional Road 97, RR 6, Flamborough', 'POINT(43.383655 -80.139379)');
INSERT INTO dbo.Beach Values (8, 'Van Wagner''s Beach', '180 Van Wagner''s Beach Road, Hamilton', 'POINT(43.259482 -79.765117)');
﻿CREATE DATABASE QuanLyQuanCafe3
GO

USE QuanLyQuanCafe3
GO

--Food
--Table
--FoodCategory
--Account
--Bill
--BillInfo

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có ten',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Tr?ng || C� ng??i
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Yen Linh',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0 -- 1: ?� thanh to�n && 0: ch?a thanh to�n
	
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO

INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'admin' , -- UserName - nvarchar(100)
          N'Yen Linh' , -- DisplayName - nvarchar(100)
          N'admin123' , -- PassWord - nvarchar(1000)
          1  -- Type - int
        )
		
INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'staff' , -- UserName - nvarchar(100)
          N'staff' , -- DisplayName - nvarchar(100)
          N'123456' , -- PassWord - nvarchar(1000)
          0  -- Type - int
        )
GO
UPDATE dbo.TableFood SET status = N'Có người' WHERE id=8
UPDATE dbo.TableFood SET status = N'Có người' WHERE id=12
GO

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO

EXEC dbo.USP_GetAccountByUserName @userName = N'admin' -- nvarchar(100)
GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO

exec dbo.USP_GetAccountByUserName @userName = N'admin'

/*them ban bang vong lap*/ --bai8
DECLARE @i INT = 1

WHILE @i <= 20
BEGIN
	INSERT dbo.TableFood ( name)VALUES  ( N'Bàn ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO
/* thu tuc lay ra danh sach ban*/
CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO


--them category
INSERT dbo.FoodCategory (name) VALUES ( N'COFFEE')
INSERT dbo.FoodCategory (name) VALUES ( N'TEA')
INSERT dbo.FoodCategory (name) VALUES ( N'MILK TEA')
INSERT dbo.FoodCategory (name) VALUES ( N'SODA')
INSERT dbo.FoodCategory (name) VALUES ( N'ICE BLENDED')
INSERT dbo.FoodCategory (name) VALUES ( N'MORE')

INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cafe đá',1, 16000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cafe sữa',1, 20000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cafe latte',1, 27000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Bạcc sĩu',1, 25000)
select * from dbo.Food
update dbo.Food set name=N'Bạc sĩu' where id=4
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà đường',2, 18000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà chanh',2, 22000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà mật ong',2, 25000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà sen nhãn',2, 32000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà sen',2, 30000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà táo',2, 26000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà vải',2, 30000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà đào',2, 30000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà olong',2, 24000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà habicus',2, 28000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà tắc',2, 20000)

INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà sữa truyền thống',3, 22000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà sữa mây',3, 25000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Trà sữa olong',3, 28000)

INSERT dbo.Food (name,idCategory,price) VALUES ( N'Soda việt quốc',4, 28000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Soda mây',4, 28000)

INSERT dbo.Food (name,idCategory,price) VALUES ( N'Chanh xay',5, 28000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cafe đá xay',5, 35000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Matcha đá xay',5, 38000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Choco đá xay',5, 40000)

INSERT dbo.Food (name,idCategory,price) VALUES ( N'Rau má',6, 18000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Đá chanh',6, 18000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cam tươi',6, 20000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cam tươi mật ong',6, 26000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cacao sữa',6, 28000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Ya-ua đá',6, 30000)
INSERT dbo.Food (name,idCategory,price) VALUES ( N'Cacao kem trứng',6, 38000)


--th�m bill
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idTable ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          8 , -- idTable - int
          0  -- status - int 0: chua checkout, 1: da checkout
        )
        

INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          idTable ,
          status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          GETDATE() , -- DateCheckOut - date
          12 , -- idTable - int
          1  -- status -  int 0: chua checkout, 1: da checkout
        )
--them BillInfo
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 1, -- idBill - int
          5, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( idBill, idFood, count )
VALUES  ( 2, -- idBill - int
          16, -- idFood - int
          1  -- count - int
          )

select * from dbo.Food
select * from dbo.FoodCategory
select * from dbo.Bill 
select * from dbo.BillInfo
select * from dbo.TableFood
 update dbo.Bill set status = 0 where id=4
 delete from dbo.Bill where id=5

 SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill as b, dbo.Food as f WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 1 AND b.idTable =9
--tong tien
SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill as b, dbo.Food as f 
WHERE bi.idBill = b.id AND bi.idFood = f.id AND status = 0 AND b.idTable = 12;
GO

-- thu tuc them mon an vao hoa don
CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN 
		INSERT dbo.Bill
			( DateCheckIn,
			  DateCheckOut,
			  idTable,
			  status
			)
		VALUES ( GETDATE(), -- DateCheckIn - date
		         NULL, -- DateCheckOut - date
				 @idTable, -- idTable - int
				 0 -- status - int, 0: chua, 1 roi
				)
END
GO

-- thu tuc them mon an vao thong tin hoa don
CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	 INSERT dbo.BillInfo
			(idBill, idFood, count )
	 VALUES (@idBill,-- idBill - int
	         @idFood, -- idFood - int
			 @count  -- count - int
			 )
END
GO


ALTER PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN 
     DECLARE @isExitsBillInfo INT
	 DECLARE @foodCount INT = 1

	 SELECT @isExitsBillInfo = id, @foodCount = b.count 
	 FROM dbo.BillInfo AS b 
	 WHERE idBill = @idBill AND idFood = @idFood

	 IF (@isExitsBillInfo > 0 )
	 BEGIN
			DECLARE @newCount INT = @foodCount + @count
			IF (@newCount > 0 )
				UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idFood = @idFood
			ELSE
			    DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	 END
	 ELSE 
	 BEGIN
				
			 INSERT dbo.BillInfo
					(idBill, idFood, count )
			 VALUES (@idBill,-- idBill - int
					 @idFood, -- idFood - int
					 @count  -- count - int
					 )
	 END
	
END
GO

update dbo.Bill 


--CLIP BAI 13
*ALTER PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN 
     DECLARE @isExitsBillInfo INT
	 DECLARE @foodCount INT = 1

	 SELECT @isExitsBillInfo = id, @foodCount = b.count 
	 FROM dbo.BillInfo AS b 
	 WHERE idBill = @idBill AND idFood = @idFood

	 IF (@isExitsBillInfo > 0 )
	 BEGIN
			DECLARE @newCount INT = @foodCount + @count
			IF (@newCount > 0 )
				UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idFood = @idFood
			ELSE
			    DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	 END
	 ELSE 
	 BEGIN
				
			 INSERT dbo.BillInfo
					(idBill, idFood, count )
			 VALUES (@idBill,-- idBill - int
					 @idFood, -- idFood - int
					 @count  -- count - int
					 )
	 END
	
END
GO
EXEC USP_InsertBill @idTable

SELECT * FROM dbo.Bill WHERE idBill =3

SELECT MAX(id) FROM dbo.Bill


GO
*/
ALTER PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN 
     DECLARE @isExitsBillInfo INT
	 DECLARE @foodCount INT = 1

	 SELECT @isExitsBillInfo = id, @foodCount = b.count 
	 FROM dbo.BillInfo AS b 
	 WHERE idBill = @idBill AND idFood = @idFood

	 IF (@isExitsBillInfo > 0 )
	 BEGIN
			DECLARE @newCount INT = @foodCount + @count
			IF (@newCount > 0 )
				UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idFood = @idFood
			ELSE
			    DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	 END
	 ELSE 
	 BEGIN
				
			 INSERT dbo.BillInfo
					(idBill, idFood, count )
			 VALUES (@idBill,-- idBill - int
					 @idFood, -- idFood - int
					 @count  -- count - int
					 )
	 END
	
END
GO
--tao trigger cap nhat thong tin hoa don
ALTER TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT

	SELECT @idBill = idBill From Inserted

	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0

    UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable
	
END
GO

CREATE TRIGGER UTG_UpdateTable
ON dbo.TableFood FOR UPDATE
AS
BEGIN
	DECLARE @idTable INT
	DECLARE @status NVARCHAR (100)
	
	SELECT @idTable = id, @status = Inserted.status FROM Inserted

	DECLARE @idBill INT
	SELECT @idBill = id FROM dbo.Bill WHERE idTable = @idTable AND status = 0

	DECLARE @coundBillInfo INT
	SELECT @coundBillInfo = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill

	IF (@coundBillInfo > 0 AND @status <> N'Có người')
		UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable
	ELSE IF (@coundBillInfo < 0 AND @status <> N'Trống')
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable

END
GO
--tao trigger ?e update hoa don
CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = id FROM Inserted	
	
	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	
	DECLARE @count int = 0
	
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0
	
	IF (@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO
select * from dbo.BillInfo
select * from dbo.Bill
update dbo.Bill set status=0  where id=4

UPDATE dbo.Bill SET status = 1, DateCheckOut= GetDate(), discount = discount WHERE id =2

delete dbo.BillInfo
delete dbo.Bill

--them thuoc tinh discount vao Bill
ALTER TABLE dbo.Bill
add discount int
-- mac dinh discount = 0
UPDATE dbo.Bill SET discount = 0
GO
--tao lai thu tuc de them discount vao
ALTER PROC USP_InsertBill
@idTable INT
AS
BEGIN 
		INSERT dbo.Bill
			( DateCheckIn,
			  DateCheckOut,
			  idTable,
			  status,
			  discount
			)
		VALUES ( GETDATE(), -- DateCheckIn - date
		         NULL, -- DateCheckOut - date
				 @idTable, -- idTable - int
				 0 ,-- status - int, 0: chua, 1 roi
				 0
				)
END
GO

-- tao thu tuc chuyen ban
ALTER PROC USP_SwitchTable1
@idTable1 INT , @idTable2 int

AS BEGIN
	DECLARE @idFirstBill int
	DECLARE @idSecondBill INT

	SELECT @idSecondBill = id FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
	SELECT @idFirstBill = id FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0
	SELECT id FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0

	PRINT @idFirstBill
	PRINT @idSecondBill
	PRINT '------------'

	IF (@idFirstBill IS NULL)
	BEGIN
			PRINT '0000001'
			INSERT dbo.Bill
			( DateCheckIn,
			  DateCheckOut,
			  idTable,
			  status
			  
			)
		VALUES ( GETDATE(), -- DateCheckIn - date
		         NULL, -- DateCheckOut - date
				 @idTable1, -- idTable - int
				 0 -- status - int, 0: chua, 1 roi 
				)
		SELECT @idFirstBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0
		
	END

	IF (@idSecondBill IS NULL)
	BEGIN 
			PRINT '000002'
			INSERT dbo.Bill
			( DateCheckIn,
			  DateCheckOut,
			  idTable,
			  status
			  
			)
		VALUES ( GETDATE(), -- DateCheckIn - date
		         NULL, -- DateCheckOut - date
				 @idTable2, -- idTable - int
				 0 -- status - int, 0: chua, 1 roi 
				)
		 SELECT @idSecondBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
		
	END
	SELECT id INTO IDBIllInfoTable FROM dbo.BillInfo WHERE idBill = @idSecondBill

	UPDATE dbo.BillInfo SET idBill = @idSecondBill WHERE idBill = @idFirstBill

	UPDATE dbo.BillInfo SET idBill = @idFirstBill WHERE id IN (SELECT * FROM IDBIllInfoTable)

	DROP TABLE IDBIllInfoTable
END
GO

-- them thuoc tinh totalPrice vao Bill
 ALTER TABLE dbo.Bill ADD totalPrice FLOAT
-- tao thu tuc xem danh sach hoa don theo ngay 
CREATE PROC USP_GetListBillByDate
 @checkIn Date, @checkOut date
 AS
 BEGIN
	SELECT t.name AS [Tên bàn], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], discount AS [Giảm giá],  b.totalPrice AS [Tổng tiền]
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut
	AND b.status = 1 AND t.id = b.idTable 
END
GO

-- cau lenh tim cu the Bill tho CheckIn , CheckOut
 SELECT t.name, b.totalPrice, DateCheckIn, DateCheckOut, discount
 FROM dbo.Bill AS b, dbo.TableFood AS t
 WHERE DateCheckIn >= '20230921' AND DateCheckOut <= '20231021'
 AND b.status = 1 AND t.id = b.idTable 
 GO

 -- thu tuc cap nhat tai khoan
CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password
	
	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END		
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
	end
END
GO

--ham de tim kiem bang chu khong dau van tim duoc 
 CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
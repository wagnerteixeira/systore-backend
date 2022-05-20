CREATE TABLE `billreceive` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClientId` int(11) NOT NULL,
  `Code` int(11) NOT NULL,
  `Quota` smallint(6) NOT NULL,
  `OriginalValue` decimal(18,2) NOT NULL,
  `Interest` decimal(18,2) NOT NULL,
  `FinalValue` decimal(18,2) NOT NULL,
  `PurchaseDate` date NOT NULL,
  `DueDate` date NOT NULL,
  `PayDate` date DEFAULT NULL,
  `DaysDelay` int(11) NOT NULL,
  `Situation` tinyint(4) NOT NULL,
  `Vendor` varchar(30) CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_billreceive_ClientId` (`ClientId`),
  CONSTRAINT `FK_billreceive_client_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `client` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
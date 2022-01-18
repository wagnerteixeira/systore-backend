CREATE TABLE `sale` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClientId` int(11) NOT NULL,
  `FinalValue` decimal(18,2) NOT NULL,
  `SaleDate` date NOT NULL,
  `Vendor` varchar(30) CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_sale_ClientId` (`ClientId`),
  CONSTRAINT `FK_sale_client_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `client` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


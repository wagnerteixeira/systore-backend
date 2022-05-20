CREATE TABLE `itemsale` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SaleId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `Quantity` decimal(18,2) NOT NULL,
  `TotalPrice` decimal(18,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_itemsale_ProductId` (`ProductId`),
  KEY `IX_itemsale_SaleId` (`SaleId`),
  CONSTRAINT `FK_itemsale_product_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `product` (`Id`),
  CONSTRAINT `FK_itemsale_sale_SaleId` FOREIGN KEY (`SaleId`) REFERENCES `sale` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
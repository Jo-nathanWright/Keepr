CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  id int NOT NULL primary key AUTO_INCREMENT,
  creatorId VARCHAR(255) NOT NULL comment 'Creator Id References Account',
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  img VARCHAR(255) NOT NULL,
  views int NOT NULL,
  shares int,
  keeps int,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 comment '';

CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL primary key AUTO_INCREMENT,
  creatorId VARCHAR(255) NOT NULL comment 'Creator Id References Account',
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  img VARCHAR(255) NOT NULL,
  isPrivate TINYINT COMMENT 'Bool value for Private',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 comment '';
CREATE TABLE IF NOT EXISTS vaultKeep(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
  creatorId VARCHAR(255) NOT NULL comment 'Creator Id References key',
  vaultId int NOT NULL comment 'vault id',
  keepId int NOT NULL comment 'keep id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
)default charset utf8 comment '';

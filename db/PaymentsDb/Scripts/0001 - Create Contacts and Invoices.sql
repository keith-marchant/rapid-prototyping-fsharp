CREATE TABLE Contacts (
	ContactId INT Identity(1,1) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
	[AccountBsb] CHAR(6) NULL,
	[AccountNumber] CHAR(10) NULL,
	[AccountName] VARCHAR(255) NULL,
	CONSTRAINT [PK_Contacts] PRIMARY KEY (ContactId)
);

CREATE TABLE PaymentStatuses (
	PaymentStatusId INT NOT NULL,
	PaymentStatus VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_PaymentStatuses] PRIMARY KEY (PaymentStatusId)
);

CREATE TABLE Invoices (
	InvoiceId INT IDENTITY(1,1) NOT NULL,
	InvoiceReference VARCHAR(255) NOT NULL,
	Amount DECIMAL(10,2) NOT NULL,
	ContactId INT NOT NULL,
	PaymentStatusId INT NOT NULL,
	DueDate DATE NOT NULL,
	CONSTRAINT [PK_Invoices] PRIMARY KEY (InvoiceId),
	CONSTRAINT [FK_Invoices_Contacts] FOREIGN KEY (ContactId) REFERENCES Contacts(ContactId),
	CONSTRAINT [FK_Invoices_PaymentStatuses] FOREIGN KEY (PaymentStatusId) REFERENCES PaymentStatuses(PaymentStatusId)
);

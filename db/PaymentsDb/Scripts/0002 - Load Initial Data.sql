INSERT INTO Contacts (Name, AccountBsB, AccountNumber, AccountName)
VALUES('ACME Enterprises', '012345', '12345678', 'ACME'),
('BETA Academies', NULL, NULL, NULL)

INSERT INTO PaymentStatuses(PaymentStatusId, PaymentStatus)
VALUES(1, 'Received'),
(2, 'Payment Pending'),
(3, 'Paid')

INSERT INTO Invoices(InvoiceReference, Amount, ContactId, PaymentStatusId, DueDate)
VALUES('REF-12345678', 99.99, 1, 2, '2022-09-28'),
('REF-9876543', 24.80, 1, 1, '2022-10-01'),
('REF-ABCDEFG', 31.99, 2, 2, '2022-10-15')

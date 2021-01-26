# CQRSEventSourcing
CQRS and Event Sourcing example

The project suggests use of the CQRS and Event Sourcing when storing information in XML file format. The idea is to understand building distributed systems where we have one central database and another one replicated database. The replicated database could do only reads, and queries but the main database accepts POSTs, PUTs, DELETEs too. This is to optimize and decouple the solution for faster consumption of data. Many things are missing here such as the usage of a message broker like Kafka or RabitMQ but that could be implemented very easily.

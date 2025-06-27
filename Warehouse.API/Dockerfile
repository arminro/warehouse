# Use official Microsoft SQL Server image as base
FROM mcr.microsoft.com/mssql/server:2019-latest

# Set environment variables
ENV ACCEPT_EULA=Y \
    SA_PASSWORD=MyVeryStrongPassword1234? \
    MSSQL_PID=Developer

# Expose SQL Server default port
EXPOSE 1433

# Start SQL Server
CMD ["/opt/mssql/bin/sqlservr"]

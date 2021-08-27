<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2657)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to calculate IDs when inserting new appointments to ASPxScheduler bound to SQL Server


<p>We will use the <a href="https://www.devexpress.com/Support/Center/p/E215">How to bind ASPxScheduler to MS SQL Server database</a> example as a starting point. We see that the ID column in the CarScheduling table is defined as an Identity Column. By default the Identity Columns automatically assigns a value for each new row that has been inserted. But what if you want to insert your own value into the column for some reason (e.g. to reuse the IDs of deleted appointments)? To do this, update the SqlDataSource.InsertCommand as follows:<br />


```aspx
<asp:SqlDataSource ID="SqlDataSourceAppointments" runat="server"<newline/>
    InsertCommand="SET IDENTITY_INSERT CarScheduling ON; INSERT INTO [CarScheduling] (ID, ...) VALUES (@ID, ...); SET IDENTITY_INSERT CarScheduling OFF;" ...<newline/>

```

That is, use the SET IDENTITY_INSERT statement to disable/enable the automatic Identity Column insertion option. Note that in this scenario the ID parameter should be added to the SqlDataSource.InsertParameters collection.<br />
In the code behind you can omit the SqlDataSource.Inserted event because you should implement your own logic to generate a unique ID. Please refer to the source code for more details.<br />
<strong>See also:</strong><br />
<a href="http://www.sqlteam.com/article/how-to-insert-values-into-an-identity-column-in-sql-server"><u></code>How to Insert Values into an Identity Column in SQL Server</u></a></p>

<br/>



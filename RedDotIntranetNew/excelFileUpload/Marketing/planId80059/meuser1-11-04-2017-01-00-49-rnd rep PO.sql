select ROW_NUMBER() Over (Order by A.autoindex) As [SrNo], A.*,B.processStatusName as action_Stage from dbo.processStatusTrack A 
join  dbo.processStatus B on B.processStatusID=A.action_StatusID and A.fk_processID=B.fk_processID 
where fk_processRequestId=(select processRequestId from dbo.processRequest where refValue='LPO-409')  order by autoindex

select refId,refValue,(refValue+'   (' + convert(varchar(10),refId)+ ')') as ShowVal from dbo.processRequest where refValue='LPO-409'


select refId,refValue,(refValue+'   (' + convert(varchar(10),refId)+ ')') as ShowVal from dbo.processRequest where refValue='LPO-409'

--LPO-409,LPO-412

--LPO-250


select * from dbo.processRequest where refId=50002

select distinct(fk_processId) from dbo.processRequest 

select * from dbo.processRequest where fk_processId in (10011,10012,10013)

select * from dbo.PurchaseOrders where poid=52042
select * from dbo.processRequest 

select R.*,P.evoPoNo from dbo.processRequest R inner join PurchaseOrders P on P.evoPoNo=R.refValue and fk_processId in (10011,10012,10013)  
order by refValue

select refId,refValue,(refValue+'   (' + convert(varchar(10),refId)+ ')') as ShowVal from dbo.processRequest where refValue='LPO-409'

select ROW_NUMBER() Over (Order by A.autoindex) As [SrNo], A.*,B.processStatusName as action_Stage from dbo.processStatusTrack A 
join  dbo.processStatus B on B.processStatusID=A.action_StatusID and A.fk_processID=B.fk_processID 
where fk_processRequestId=(select processRequestId from dbo.processRequest where refId=51913)  order by autoindex

-------------

------select R.refId,R.refValue,(R.refValue+' [' + convert(varchar(10),P.totalSelling) +']   (' + convert(varchar(10),R.refId)+ ')') as ShowVal,P.totalSelling 
------from PurchaseOrders P
------left join dbo.processRequest R on P.poid=R.refId Where P.evoPoNo='LPO-409'


select * from dbo.PurchaseOrders where poid=52042
select * from dbo.processRequest where refId=52042
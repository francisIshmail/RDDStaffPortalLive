// JScript File

 //Function -1.        
         //call method on image click to popup the calendar
         function calendarPicker(frmId,imgId,txtBoxId) //param is the name of the recipient textbox
            {
                        //pass your (form and text field name as param) as a string value to str='your Field Name' as below
                        //var str=frmId + '.'+ txtBoxId;
                        var str= txtBoxId;
                        var arr=findControlPos(imgId); //value of returned as array
                        
                        TheNewWin=window.open("../Calendarr.aspx?field=" + str,"calendarPopup","width=322,height=264,status=0,titlebar=0,toolbar=0,resizable=no,scrollbars=0",true);
                        
                         var NewWinLeft=arr[0];
                         var NewWinTop=arr[1]; 
                         
                         TheNewWin.moveTo(NewWinLeft,NewWinTop);
           }
           
   //Function 0.
             //method returns the left,top positions of image which is clicked to popup the calendar
             function findControlPos(imgId)  
             {
                var Controlobj=document.getElementById(imgId);
	            var curleftPos =  0;
                    var curtopPos = 0;
	            if (Controlobj.offsetParent)
                    {
                     do {
                     
        	            curleftPos += Controlobj.offsetLeft;
		                curtopPos += Controlobj.offsetTop;
     	                } while (Controlobj = Controlobj.offsetParent);
                    }
               //........................Logic to popup window at right position
                    var currWinWidth = 0;
                    var currWinHeight = 0;
                    currWinWidth = document.documentElement.clientWidth;
                    currWinHeight = document.documentElement.clientHeight;
                    
                        if(curleftPos+234 <= currWinWidth)
                          curleftPos=curleftPos+66; 
                         else
                          curleftPos=curleftPos-234+46;
                         
                         if(curtopPos-204 >= 0)
                           curtopPos=curtopPos-165;  
                         else
                           curtopPos=curtopPos+204-140;
              //........................
              
              return [curleftPos+50,curtopPos+50];
            }
            
 //Function 1.           
            function getNewDate(caller)
            {
              var arr=['January','February','March','April','May','June','July','August','September','October','November','December'];
              var mth=0;
              for(var i=0;i<arr.length;i++) 
              {
               if(document.getElementById("lblMonth").innerText==arr[i])
                break;
              }
              mth=i+1;
                             
               if(mth==12 && caller=="previousMonth")
               {
                 var yr=Number(document.getElementById("lblYear").innerText);
                 if(yr>1)
                  document.getElementById("lblYear").innerText=yr-1;
               }
               
               if(mth==1 && caller=="nextMonth")
               {
                var yr=Number(document.getElementById("lblYear").innerText);
                if(yr<9999)
                document.getElementById("lblYear").innerText=yr+1;
               }
            
             var nDate= mth + '/'+ document.getElementById("lblDate").innerText + '/'+ document.getElementById("lblYear").innerText;
             document.getElementById("txtNewDate").value=nDate;
             document.getElementById("BtnSetCalendarDate").click();
            }
            
            //Function 2.
             function NextPrevMonth(cMonth,cMove)
             {
                var returnMonth='nil';
                var arr=['January','February','March','April','May','June','July','August','September','October','November','December'];
                
                for(var i=0;i<arr.length;i++) 
                {
                     if(cMonth==arr[i] && cMove=="previousMonth")
                     {
                        if(i==0)
                         returnMonth=arr[i+11];
                        else
                         returnMonth=arr[i-1];
                     }    
                     
                     if(cMonth==arr[i] && cMove=="nextMonth")
                     {
                       if(i==11)
                         returnMonth=arr[i-11];
                       else 
                       returnMonth=arr[i+1];
                     }
                 }    
              return returnMonth;
             }
            
            //Function 3.
            function funImgLeft()
            {
             var mth=document.getElementById("lblMonth").innerText;
             document.getElementById("lblMonth").innerText=NextPrevMonth(mth,"previousMonth");
             getNewDate("previousMonth");
            }
            
            //Function 4.
            function funImgRight()
            {
             var mth=document.getElementById("lblMonth").innerText;
             document.getElementById("lblMonth").innerText=NextPrevMonth(mth,"nextMonth");
             getNewDate("nextMonth");
            }
            
            //Function 5.
            function funImgUp()
            {
             var yr=Number(document.getElementById("lblYear").innerText);
             if(yr<9999)
              document.getElementById("lblYear").innerText=yr+1;
             getNewDate("nextYear");
            }
            
            //Function 6.
            function funImgDown()
            {
             var yr=Number(document.getElementById("lblYear").innerText);
             if(yr>1)
              document.getElementById("lblYear").innerText=yr-1;
             getNewDate("previousYear");
            }
            
         //Function 7.
        function de_highlightWeek()  
         {
            
            var tblId = "Calendar1";
            var tbl = document.getElementById(tblId);
	        var tblBodyObj = tbl.tBodies[0];
	        
	            for (var i=2; i<tblBodyObj.rows.length; i++) //count of rows in calander table
	             {
                   for (var c=0; c<tblBodyObj.rows[i].cells.length; c++)
                   {
                       var newc = tblBodyObj.rows[i].cells[c];
                       
                       newc.style.fontWeight= 'normal';
                       newc.style.color='black';
                       newc.style.background = 'url(../images/noimg.jpg) no-repeat center';
                      
                       var lnk=newc.firstChild;
                       lnk.style.color='black';
                    }
                }
		  }

        //Function 8.
        function highlightWeek(tblId, r,imgNm)
        {
            r=r+1;
            var tbl = document.getElementById(tblId);
	        var tblBodyObj = tbl.tBodies[0];
	        var flgS='f';
	        var prevVal=1;
	        if(r==1)
	        {
	            for (var i=2; i<tblBodyObj.rows.length; i++) //count of rows in calander table
	            {
                   for (var c=0; c<tblBodyObj.rows[i].cells.length; c++)
                   {
                       var newc = tblBodyObj.rows[i].cells[c];
                      
                       if(newc.innerText=='1' && flgS=='f')
                       {
                        flgS='t';
                       }
                       if(flgS=='t'&& prevVal<=newc.innerText)
                       {
                               newc.style.fontWeight= 'bold';
                               newc.style.color='red';
                               newc.style.background = 'url(../images/imgCellBg.jpg) no-repeat center';
                               
                               var lnk=newc.firstChild;
                               lnk.style.color='red';
                               prevVal=Number(newc.innerText);
                       }
                       else
                        {
                         newc.style.fontWeight= 'normal';
                         newc.style.color='black';
                         newc.style.background = 'url(../images/noimg.jpg) no-repeat center'
                         
                         var lnk=newc.firstChild;
                         lnk.style.color='black';
                        }
                    }
                }
		    }
	        
	        if(r>1)
	        {
	            for (var i=2; i<tblBodyObj.rows.length; i++) //count of rows in calander table
	            {
                   for (var c=0; c<tblBodyObj.rows[i].cells.length; c++)
                   {
                       var newc = tblBodyObj.rows[i].cells[c];
                       if(i==r)
                        {
                          newc.style.fontWeight= 'bold';
                          newc.style.color='red';
                          newc.style.background = 'url(../images/imgCellBg.jpg) no-repeat center';
                          
                          var lnk=newc.firstChild;
                          lnk.style.color='red';
                        }
                        else
                        {
                         newc.style.fontWeight= 'normal';
                         newc.style.color='black';
                         newc.style.background = 'url(../images/noimg.jpg) no-repeat center'
                         
                         var lnk=newc.firstChild;
                         lnk.style.color='black';
                        }
                    }
		        }
		    }
        }



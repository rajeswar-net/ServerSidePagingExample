$(document).ready(function () {
    $('#salesorders').DataTable
    ({
        "columnDefs": [
          {
              "width": "5%",
              "targets": [0]
          },
          {
              "className": "text-center custom-middle-align",
              "targets": [0, 1, 2, 3, 4, 5, 6]
          }, ],
        "language":
        {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
        },
        "processing": true,
        "serverSide": true,
        "ajax":
      {
          "url": "/Home/getSalesOrders",
          "type": "POST",
          "dataType": "JSON"
      },
        "columns": [
          {
              "data": "Sr"
          },
          {
              "data": "OrderTrackNumber"
          },
          {
              "data": "Quantity"
          },
          {
              "data": "ProductName"
          },
          {
              "data": "SpecialOffer"
          },
          {
              "data": "UnitPrice"
          },
          {
              "data": "UnitPriceDiscount"
          }]
    });

    $("#employeeGrid").DataTable({  
  
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "pageLength": 5,  
  
        "ajax": {  
            "url": "/Employee/LoadEmployeeData",  
            "type": "POST",  
            "datatype": "json"  
        },  
  
        "columnDefs":  
        [{  
            "targets": [0],  
            "visible": false,  
            "searchable": false  
        }
        //,  
        //{  
        //    "targets": [7],  
        //    "searchable": false,  
        //    "orderable": false  
        //},  
        //{  
        //    "targets": [8],  
        //    "searchable": false,  
        //    "orderable": false  
        //},  
        //{  
        //    "targets": [9],  
        //    "searchable": false,  
        //    "orderable": false  
            //}
        ],  
  
        "columns": [  
              { "data": "employee_id", "name": "employee_id", "autoWidth": true },  
              { "data": "first_name", "name": "first_name", "autoWidth": true },  
              { "data": "last_name", "name": "ContactName", "autoWidth": true },  
              { "data": "sex", "name": "sex", "autoWidth": true },  
              { "data": "age", "name": "age", "autoWidth": true },  
              { "data": "start_date", "name": "start_date", "autoWidth": true },  
              { "data": "salary", "name": "salary", "autoWidth": true },  
              {  
                  "render": function (data, type, full, meta)  
                  { return '<a class="btn btn-info" href="/Employee/Edit/' + full.employee_id + '">Edit</a>'; }  
              },  
               {  
                   data: null, render: function (data, type, row) {  
                       return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.employee_id + "'); >Delete</a>";  
                   }  
               },  
  
        ]  
  
    });  
});  

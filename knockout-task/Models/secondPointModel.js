(function(){
    var DataGrid = function(data) {
        var self = this;

        self.data = ko.observableArray(data);

        self.renderNewRow = function(row) {
            self.data.push(row);
        };

        self.updateRow = function(row) {
            var itemToUpdate = ko.utils.arrayFirst(self.data(), function(item) {
                return item.id === row.id;
            });

            self.data.replace(itemToUpdate, row);
        };
        
        self.deleteRow = function(id) {
            self.data.remove(function(item) { return item.id === id });
        };
        
        self.useUpdateFunction = function(item) {
            var newSecondRow = {
                id:1,
                nestedData: [
                    { field1 : "U", field2 : "5y", field3 : "5g", field4: "1d" },
                    { field1 : "2u", field2 : "2d", field3 : "y", field4: "5t" },
                    { field1 : "3u", field2 : "y", field3 : "u", field4: "3f" },
                ]
            };

            self.updateRow(newSecondRow);
        }

        self.useDeleteFunction = function() {
            self.deleteRow(0);
        }

        self.useRenderFunction = function() {
            var newItem = {
                id : 3,
                nestedData: [
                    {field1 : "9", field2 : "8", field3 : "7", field4: "6"},
                    {field1 : "91", field2 : "82", field3 : "72", field4: "6"}
                ]
            };

            self.renderNewRow(newItem);
        }
    };

    var data = new DataGrid([
        { 
            id: 0, 
            nestedData: [
                {field1 : "1", field2 : "2", field3 : "3", field4: "4"},
                {field1 : "2", field2 : "2", field3 : "3", field4: "4"},
                {field1 : "3", field2 : "2", field3 : "3", field4: "4"},
                
            ]
        },
        { 
            id: 1, 
            nestedData: [
                {field1 : "5", field2 : "2", field3 : "3", field4: "4"},
                {field1 : "6", field2 : "2", field3 : "3", field4: "4"},
                {field1 : "7", field2 : "2", field3 : "3", field4: "4"},
                {field1 : "8", field2 : "2", field3 : "3", field4: "4"},
                {field1 : "9", field2 : "2", field3 : "3", field4: "4"}
            ]
        },
    ]);

    ko.applyBindings(data, document.getElementById('secondPart'));
}())
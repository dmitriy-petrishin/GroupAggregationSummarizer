(function() {
    var TableRowModel = function(columns) {
        var self = this;
        self.columns = ko.observableArray(columns);

        self.useAddColumns = function() {
            var object = [
                {id:4, name: "dsfdfs"},
                {id:5, name: "sfdsffds"},
                {id:6, name: "3twt3wt"}
            ]
            self.addColumns(object);
        }

        self.addColumns = function(columns) {
            console.log(columns);
            columns.forEach(element => {
                self.columns.push(element)
            });
        };
    };

    var columns = new TableRowModel([
        {id: 1, name: "Ivan" },
        {id: 2, name: "Neivan" },
        {id: 1, name: "Ivan" },
        {id: 1, name: "Philipp" }
    ]);

    ko.applyBindings(columns, document.getElementById('firstPart'));
}())
//, attr: {'rowspan': 'nestedData.length'}
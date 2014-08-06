function CurrencyStampsViewModel() {
    var self = this;

    self.displayDate = ko.observable();
    self.currencyStamps = ko.observableArray([]);

    self.refreshStamps = function () {

        $.ajax({
            url: '/CurrencyStamps.ashx?displayDate={displayDate}'.replace('{displayDate}', self.displayDate())
        })
        .done(function (stampsArray) {
            self.currencyStamps(stampsArray);
        });
        
    };

    
}




ko.applyBindings(new CurrencyStampsViewModel());
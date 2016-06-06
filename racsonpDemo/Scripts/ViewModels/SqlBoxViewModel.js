


function SqlBoxViewModel(sqlBox) {
    var self = this;

    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);

  //  self.isCreating = author.id == 0;

    self.sqlBox = {
       successful: ko.observable(sqlBox.successful),
        message: ko.observable(sqlBox.message),
        query: ko.observable(sqlBox.query),
        format: ko.observable(sqlBox.format),
        dataTable: ko.observable(sqlBox.dataTable),
    };

    self.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;

        self.sending(true);

        // include the anti forgery token
        self.sqlBox.__RequestVerificationToken = form[0].value;

        //$.ajax({
        //    url: '/api/authors',
        //    type: (self.isCreating) ? 'post' : 'put',
        //    contentType: 'application/json',
        //    data: ko.toJSON(self.sqlBox)
        //})


        $.ajax({
            url: 'Consulta',
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: ko.toJS(self.sqlBox)
        })

        .success(self.successfulSave)
        .error(self.errorSave)
        .complete(function () { self.sending(false) });


     





    };

    self.successfulSave = function () {
        self.saveCompleted(true);

        $('.body-content').prepend('<div class="alert alert-success"><strong>Success!</strong> The sqlBox has been saved.</div>');
        setTimeout(function () {
            if (self.isCreating)
                location.href = './';
            else
                location.href = '../';
        }, 1000);
    };

    self.errorSave = function () {
        $('.body-content').prepend('<div class="alert alert-danger"><strong>Error!</strong> There was an error saving the sqlBox.</div>');
    };
}
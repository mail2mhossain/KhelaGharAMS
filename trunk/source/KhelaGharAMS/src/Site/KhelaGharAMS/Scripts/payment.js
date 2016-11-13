var paymentModule = (function (window, undefined) {

    function isValid() {
        if (!parseFloat($('#amount').val())) {
            $('#amountError').text('invalid amount');
            return false;
        } else {
            $('#amountError').text('');
            return true;
        }

    }

    function callPayOnline() {
        if (isValid()) {
            $.ajax({
                type: "POST",
                url: $("#onlinePaymentUrl").val(),
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ meterNo: $('#meterNo').val(), amount: $('#amount').val() }),
                dataType: "json",
                success:
                    function (data) {
                        if (data.IsSuccess) {
                            $('input[name=total_amount]').val(data.Amount);
                            $('input[name=tran_id]').val(data.TransactionNo);
                            $('input[name=success_url]').val(data.SuccessUrl);
                            $('input[name=fail_url]').val(data.CancelUrl);
                            $('input[name=cancel_url]').val(data.FailUrl);
                            $("#payment_gw").submit();
                        }

                    },
                error: function () { }
            });
        }
    }

    var bindFunctions = function () {
        $("#payOnline").on("click", callPayOnline);
    };

    var init = function () {
        bindFunctions();
    };

    return {
        Init: init
    };
})(window);

var paymentModuleMobile = (function (window, undefined) {

    function isValid() {
        if (!parseFloat($('#amount').val())) {
            $('#amountError').text('invalid amount');
            return false;
        } else {
            $('#amountError').text('');
            return true;
        }

    }

    function callPayOnline() {
        
        if (isValid()) {
            $.ajax({
                type: "POST",
                url: $("#onlinePaymentMobileUrl").val(),
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ meterNo: $('#meterNo').val(), amount: $('#amount').val() }),
                dataType: "json",
                success:
                    function (data) {
                        if (data.IsSuccess) {
                            $('input[name=total_amount]').val(data.Amount);
                            $('input[name=tran_id]').val(data.TransactionNo);
                            $('input[name=success_url]').val(data.SuccessUrl);
                            $('input[name=fail_url]').val(data.CancelUrl);
                            $('input[name=cancel_url]').val(data.FailUrl);
                            $("#payment_gw").submit();
                        }

                    },
                error: function () { }
            });
        }
    }

    var bindFunctions = function () {
        $("#payOnline").on("click", callPayOnline);
    };

    var initMobile = function () {
        bindFunctions();
    };

    return {
        InitMobile: initMobile
    };
})(window);
$("input[name*='priceFull']").keypress(function(e){
    var symbol = (e.which) ? e.which : e.keyCode;
    if (symbol < 48 || symbol > 57)  return false;
});
window.addEventListener("message", listenerMessages);
window.addEventListener("DOMContentLoaded", contentLoad);

function listenerMessages(event) {

    formBox.windowParentFrame = event.source;
    formBox.windowOrigin = event.origin;

    if (event.data == 'getHeight') {

        sendFrameData();
    } else {
        try {
            var data = JSON.parse(event.data);

            if ('action' in data) {

                if (data.action == 'setPrice') {

                    formBox.price = data.price;

                    creditFormBox.setPrice();
                }
            }

        } catch (e) {

        }
    }
}

function getSizes() {

    var scrollHeight = Math.max(
        document.body.scrollHeight, // document.documentElement.scrollHeight,
        document.body.offsetHeight, // document.documentElement.offsetHeight,
        document.body.clientHeight, // document.documentElement.clientHeight
    );

    return scrollHeight;
}

function sendFrameData() {

    formBox.sendData.action = 'setHeight';
    formBox.sendData.height = getSizes();
    formBox.windowParentFrame.postMessage(JSON.stringify(formBox.sendData), formBox.windowOrigin);
}

var formBox = {
    windowParentFrame: {},
    windowOrigin: '',
    sendData: {},
    price: '',
    acceptRules: false,
    ajaxReqest: false,
    formSetFields: {}
}

var creditFormBox  = new CreditForm();
function contentLoad() {
    creditFormBox = new CreditForm();
}

function CreditForm() {

    var ajaxFile = '/content/content/sendEmailCreditCalculator';
    var formContainer;

    init();

    function init() {

        formContainer = document.querySelector('.creditForm__form');

        formContainer.addEventListener('click', actionClick);
        formContainer.addEventListener('input', actionInput);
    }

    function actionClick(e) {
        if(~e.target.className != 'undefined') {
            if (~e.target.className.indexOf('toPageTwo')) {
                openSecondPage();
            }
            if (~e.target.className.indexOf('toPageOne') || ~e.target.className.indexOf('pass')) {
                openFirstPage();
            }

            if (~e.target.className.indexOf('termCol')) {
                activationTerm(e.target);
            }
            if (~e.target.className.indexOf('acceptedCheck')) {
                activationAccept(e.target);
            }
            if (~e.target.className.indexOf('sendBid')) {
                bidSend();
            }
            if (~e.target.className.indexOf('buttonLast')) {
                resultShowClose();
            }
            if(~e.target.className.indexOf('acceptedCheck')){
                e.target.classList.add("check");
            }
        }


    }

    function actionInput(event) {

        if (~event.target.name.indexOf('priceFull')) {

            calcPayments(event);
        }
    }

    function openSecondPage() {

        var tabs = formContainer.querySelectorAll('.creditForm__tabs li'),
            pages = formContainer.querySelectorAll('.creditForm__page');

        var check = checkFirstPage();
        if (!check) {
            return false;
        }
        for (var i = 0; i < tabs.length; i++) {

            if (i == 1) {
                tabs[i].classList.add('open');
                pages[i].classList.add('open');
            } else {
                tabs[i].classList.remove('open');
                tabs[i].classList.add('pass');
                pages[i].classList.remove('open');
            }
        }
        sendFrameData();
    }

    function checkFirstPage() {

        var result = false;

        var form = document.forms.bidForm,
            elems = form.elements;

        var price = elems.priceFull.value.trim();

        if (price.length === 0 || !isNumeric(price)) {
            elems.priceFull.classList.add('require');
        } else {
            elems.priceFull.classList.remove('require');
            result = true;
        }

        return result;
    }

    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    function openFirstPage() {

        var tabs = formContainer.querySelectorAll('.creditForm__tabs li'),
            pages = formContainer.querySelectorAll('.creditForm__page');

        for (var i = 0; i < tabs.length; i++) {

            if (i == 0) {
                tabs[i].classList.add('open');
                tabs[i].scrollIntoView();
                tabs[i].classList.remove('pass');
                pages[i].classList.add('open');
            } else {
                tabs[i].classList.remove('open');
                pages[i].classList.remove('open');
            }
        }

    }

    this.setPrice = function () {

        var form = document.forms.bidForm,
            elems = form.elements;

        elems.priceFull.value = formBox.price;
        paymentCalcPrice(formBox.price);
    }

    function activationTerm(el) {

        var rows = el.parentElement.parentElement.children;

        for (var i = 0; i < rows.length; i++) {
            rows[i].classList.remove('active');
        }
        el.parentElement.classList.add('active');
    }

    function calcPayments(event) {

        var price = event.target.value.trim();

        if (~price.indexOf(',')) {
            event.target.value = price.replace(',', '.');
        }

        if (price.length === 0 || !isNumeric(price)) {
            event.target.classList.add('require');
            paymentSetEmpty();
        } else {
            event.target.classList.remove('require');
            paymentCalcPrice(price);
        }
    }

    function paymentSetEmpty() {

        var fields = document.querySelectorAll('.creditForm__termCol:last-child');
        for (var i = 0; i < fields.length; i++) {
            fields[i].innerHTML = '-';
        }
    }
    function paymentCalcPrice(price) {

        var rows = document.querySelectorAll('.creditForm__term'),
            rate, payment;

        for (var i = 0; i < rows.length; i++) {

            var priceBox = rows[i].querySelector('.creditForm__termCol:last-child');
            if (priceBox) {

                rate = 1 + (+rows[i].dataset.rate * +rows[i].dataset.payments) / 100;
                payment = price * rate / +rows[i].dataset.payments;

                priceBox.innerHTML = payment.toFixed(2);
            }
        }
    }

    function activationAccept(checkBox) {

        if (checkBox.classList.contains('check')) {

            checkBox.classList.remove('check');
            formBox.acceptRules = false;
        } else {

            checkBox.classList.add('check');
            checkBox.classList.remove('error');
            formBox.acceptRules = true;
        }
    }

    function bidSend() {

        var checkFields = bidCheckFields();

        if (checkFields) {

            var formData = new FormData();
            var loanData = loanDataGet();

            formData.append("request", 'orderBid');
            formData.append("LOAN_PAYMENT", 'steps: ' + loanData.payments + ', ' + loanData.rate + '%');

            for (var key in formBox.formSetFields) {
                formData.append(key, formBox.formSetFields[key]);
            }

            if (formBox.ajaxReqest)
                return false;
            formBox.ajaxReqest = true;

            var request = new XMLHttpRequest();
            request.open("POST", ajaxFile)
            request.send(formData);

            request.onload = function () {

                if (request.status == 200) {

                    $(".diagnostic-popup").css({display:"block"});

                    setTimeout(function(){
                        $(".diagnostic-popup").css({display:"none"});
                    },3000);

                    document.getElementById('bidForm').style = 'display:none;';

                    var answer;
                    formBox.ajaxReqest = false;

                    try {
                        answer = JSON.parse(request.responseText);

                        if (answer.status) {

                            resultShow();
                        }
                    } catch (e) {
                    }
                }
            }
        } else {
            if (!formBox.acceptRules) {
                setErrorAccept();
            }
        }
    }

    function bidCheckFields() {

        var res = true,
            errorFields = [];

        formBox.formSetFields = {};

        var form = document.forms['bidForm'],
            elems = form.elements,
            scrollError = false,
            noPassportError = false;
            noPassportIdError = false;


        for (var i = 0; i < elems.length; i++) {

            var value = elems[i].value.trim();

            if (value.length == 0 && elems[i].dataset.require > 0) {

                if (elems[i].name == 'WAY_INFORM') {

                    var wayLabel = document.querySelector('.desc-way');
                    wayLabel.classList.add('require');
                } else {
                    elems[i].classList.add('require');

                    if(!scrollError){
                        scrollError = true;
                        elems[i].scrollIntoView();
                    }

                    res = false;
                    errorFields.push(elems[i].name);
                }
            } else {

                if (elems[i].name == 'INN') {

                    var statusInn = checkINN(value);

                    if (statusInn.valid) {

                        elems[i].classList.remove('require');
                        formBox.formSetFields[elems[i].name] = value;

                    } else {
                        elems[i].classList.add('require');
                        res = false;
                        errorFields.push(i);
                    }
                }else if(elems[i].name == 'email'){
                    if(!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(elems[i].value)){
                        elems[i].classList.add('require');
                        res = false;
                        errorFields.push(i);
                    }else{
                        elems[i].classList.remove('require');
                        formBox.formSetFields[elems[i].name] = value;
                    }
                }else {
                    elems[i].classList.remove('require');
                    formBox.formSetFields[elems[i].name] = value;
                }
            }
        }

        noPassportError = elems['passportSeria'].value !='' && elems['passportNum'].value!='';
        noPassportIdError = elems['PASSPORT_ID'].value !='' && elems['PASSPORT_ID'].value!='';


        if (!noPassportError && !noPassportIdError){
            if(!noPassportError) {
                elems['passportSeria'].classList.add('require');
                elems['passportNum'].classList.add('require');
            }
            if (!noPassportIdError){
                elems['PASSPORT_ID'].classList.add('require');
                elems['PASSPORT_ID_SOURCE'].classList.add('require');
            }
        }else{
            elems['passportSeria'].classList.remove('require');
            elems['passportNum'].classList.remove('require');
            elems['PASSPORT_ID'].classList.remove('require');
            elems['PASSPORT_ID_SOURCE'].classList.remove('require');
        }

        // PASSPORT_SERIA + PASSPORT_NUM = PASSPORT_ID
        var valuePS = elems['passportSeria'].value.trim(),
            valuePN = elems['passportNum'].value.trim(),
            valuePI = elems['PASSPORT_ID'].value.trim();

        if (valuePS.length != 0 && valuePN.length != 0) {

            elems['PASSPORT_ID'].classList.remove('require');
            var ids = errorFields.indexOf('PASSPORT_ID');
            if (ids != -1) {
                errorFields.splice(ids, 1);
            }
        } else {

            if (valuePI.length != 0) {
                elems['passportSeria'].classList.remove('require');
                elems['passportNum'].classList.remove('require');
                var ids = errorFields.indexOf('passportSeria');
                if (ids != -1) {
                    errorFields.splice(ids, 1);
                }
                var ids = errorFields.indexOf('passportNum');
                if (ids != -1) {
                    errorFields.splice(ids, 1);
                }
            }
        }

        if (errorFields.length == 0) {
            res = true;
        }

        return res;
    }

    function setErrorAccept() {

        var acceptedCheck = document.querySelector('.acceptedCheck');
        if (!acceptedCheck) {
            return false;
        }
        acceptedCheck.classList.add('error');
    }

    function checkINN(numToCheck) {

        var status = {
            error: '',
            valid: false
        };

        var numN = parseInt(numToCheck),
            numToCheckS = numToCheck + '',
            numS = numN + '';

        if (Object.is(numN, NaN)) {

            status.error = 'not integer';
            return status;
        }
        if (numS.length != numToCheckS.length) {

            status.error = 'entering not numeral letter';
            return status;
        }
        if (numS.length != 10) {

            status.error = 'entering not 10 numerals';
            return status;
        }

        var controlSum = numS[0] * (-1) + numS[1] * 5 + numS[2] * 7 + numS[3] * 9 + numS[4] * 4 + numS[5] * 6 + numS[6] * 10 + numS[7] * 5 + numS[8] * 7,
            controlNum = controlSum - (Math.floor(controlSum / 11) * 11);

        if (controlNum == 10) {
            controlNum = 0;
        }
        if (numS[9] != controlNum) {

            status.error = 'error control letter';
            return status;
        }

        status.valid = true;
        return status;
    }

    function loanDataGet() {

        var loan = {
            payments: 0,
            rate: 0
        };

        var row = document.querySelector('.creditForm__term.active');
        if (row) {
            loan.payments = row.dataset.payments;
            loan.rate = row.dataset.rate;
        }

        return loan;
    }

    function resultShow() {

        var form = document.forms['bidForm'],
            elems = form.elements;

        for (var i = 0; i < elems.length; i++) {

            if (elems[i].type == 'text' || elems[i].type == 'email') {
                elems[i].value = '';
            }
        }

        var pageSecond = document.querySelector('.creditForm__pageSecond');
        var pageLast = document.querySelector('.loanBidStatus');

        pageSecond.style.display = 'none';
        pageLast.style.display = 'block';
    }
    function resultShowClose() {

        var pageSecond = document.querySelector('.creditForm__pageSecond');
        var pageLast = document.querySelector('.loanBidStatus');

        pageSecond.style.display = 'block';
        pageLast.style.display = 'none';

        openFirstPage();
    }
}
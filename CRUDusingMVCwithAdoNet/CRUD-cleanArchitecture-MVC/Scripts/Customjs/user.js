$(function () {

    //$("#datepicker").datepicker({
    //    maxDate: 0,
    //    changeMonth: true,
    //    changeYear: true,
    //    showAnim: 'slideDown',
    //    dateFormat: 'yy-mm-dd'
    //}).on('change', function () {
    //    var age = getAge(this);
    //    $('#age').val(age);
    //    /* $('#age').val(age);*/
    //    console.log(age);
    //});
    //$("#date-from").datepicker({
    //    maxDate: 0,
    //    changeMonth: true,
    //    changeYear: true,
    //    showAnim: 'slideDown',
    //    dateFormat: 'yy-mm-dd'
    //});
  
    // datepicker
    //$("#datepicker").datepicker({
    //    maxDate: 0,
    //    changeMonth: true,
    //    changeYear: true,
    //    showAnim: 'slideDown',
    //    dateFormat: 'yy-mm-dd'
    //}).on('change', function () {
    //    var age = getAge(this);
    //    $('#age').val(age);
    //    /* $('#age').val(age);*/
    //    console.log(age);
    //});
    //// Get Age from Date
    //function getAge(dateVal) {
    //    var birthday = new Date(dateVal.value),
    //        today = new Date(),
    //        ageInMilliseconds = new Date(today - birthday),
    //        years = ageInMilliseconds / (24 * 60 * 60 * 1000 * 365.25)
    //    return Math.floor(years);
    //}
    // Image Preview 
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]); // convert to base64 string
        }
    }
    // Image change Function
    $("#imgInp").change(function () {
        $('#filediv').show();
        readURL(this);
    });
    // Get StateList from CountryId
    $("#CountryId").change(function () {
        var countryId = $(this).val();
        $("#StateId").empty();
        $.get('/Home/GetStateListbyCountryId',
            { countryId: countryId }, function (data) {
                var v = "";
                v = "<option value='' selected='selected'>-- Select State --</option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });

                $("#StateId").html(v);
            });
    });
    // Get CityList from StateId
    $("#StateId").change(function () {
        var stateId = $(this).val();
        $("#CityId").empty();
        $.get('/Home/GetCityListbyStateId',
            { stateId: stateId }, function (data) {
                var v = "";
                v = "<option value='' selected='selected'>-- Select City --</option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });
                $("#CityId").html(v);
            });
    });
  
});
 // will trigger when the document is ready
//$(document).ready(function () {
//    //FormData object  
//    $('datepicker').datepicker({
//        format: 'dd/mm/yyyy',

//        keyboardNavigation: false,
//        forceParse: false,
//        calendarWeeks: true,
//        autoclose: true,

//    });
//});  
//$('.datepicker').datepicker(); //Initialise any date pickers

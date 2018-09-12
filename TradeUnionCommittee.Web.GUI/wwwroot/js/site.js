$('input[type=checkbox]').change(function () {

    if(!this.classList.contains("dinamic"))
    {
        return;
    }

    if ($('.' + this.id)[0].classList.contains("invisible"))
    {
        $('.' + this.id)[0].classList.remove("invisible");
    } 
    else 
    {
        $('.' + this.id)[0].classList.add("invisible");
    }
});

$('input[name=TypeAccommodation]').change( function() 
{ 
    if(($('fieldset.appartment:not(.form-group.appartment.invisible)')[0]) != undefined) 
    {
        $('fieldset.appartment:not(.form-group.appartment.invisible)')[0].classList.add('invisible');
    }

    $('.' + this.id)[0].classList.remove('invisible');
});

if ($('input[name=TypeAccommodation]:checked').val())
{
    var checkedRadio = $('input[name=TypeAccommodation]:checked').val();
    $('.' + checkedRadio)[0].classList.remove('invisible');
}

$('input[type="checkbox"]:checked').each(function ()
{
    var checkedChekbox = $(this).attr('id');
    $('.' + checkedChekbox)[0].classList.remove('invisible');
});

$('#editable-select-LevelEducation').editableSelect();

$('#editable-select-Study').editableSelect();

$('#editable-select-ScientifickDegree').editableSelect();

$('#editable-select-ScientifickTitle').editableSelect();

$(function ()
{
    $("#mobile-phone").mask("+38(099)999-99-99");
    $("#home-phone").mask("999-99-9");
    $("#home-phone1").mask("999-99-99");
    $("#Mechnikov-code").mask("9-999999-999999");
    $("#ident-code").mask("9999999999");
    $("#year").mask("9999");
    $("#year-education").mask("9999");
});

//------------------------------------------------------------------------------------------------------------------------------------------

$(document).ready(function ()
{
    if (window.location.pathname === "/Employee/Create")
    {
        var idSubordinateSubdivision = { id: $("#selectMain").val() };
        $.post("/Employee/GetSubordinateSubdivision", idSubordinateSubdivision, getSubordinateSubdivision);
    }
    if (window.location.pathname === "/Search")
    {
        document.getElementById("dormitory").checked = true;
        document.getElementById("idDepartmental").disabled = true;
        document.getElementById("idDormitory").disabled = false;

        document.getElementById("employeeBirthDate").checked = true;
        document.getElementById("employeeHobby").checked = true;

        document.getElementById("home-phone").value = "";
        document.getElementById("home-phone1").value = "";
    }
});

$("#selectMain").change(function ()
{
    var idSubordinateSubdivision = { id: $(this).val() };
    $.post("/Employee/GetSubordinateSubdivision", idSubordinateSubdivision, getSubordinateSubdivision);
});

function getSubordinateSubdivision(subordinateSubdivision)
{
    document.getElementById("subordinateSubdivision").innerHTML = " ";
    if (subordinateSubdivision !== null)
    {
        //$("#subordinateSubdivision").append("<option>Виберіть кафедру</option>");
        $("#subordinateSubdivision").append("<option value value = null >Виберіть кафедру</option>");
        $.each(subordinateSubdivision, function ()
        {
            $("#subordinateSubdivision").append("<option value=" + this.hashId + ">" + this.name + "</option>");
        });
    }
    else
    {
        $("#subordinateSubdivision").append("<option>Кафедри відсутні</option>");
    }
}

//------------------------------------------------------------------------------------------------------------------------------------------

$('input[name=CityPhone]').change(function ()
{
    if ($(this).val())
    {
        document.getElementById('home-phone1').readOnly = true;
    }
    else
    {
        document.getElementById('home-phone1').readOnly = false;
    }
});

$('input[name=CityPhoneAdditional]').change(function ()
{
    if ($(this).val())
    {
        document.getElementById('home-phone').readOnly = true;
    }
    else
    {
        document.getElementById('home-phone').readOnly = false;
    }
});

//------------------------------------------------------------------------------------------------------------------------------------------

$('input[value=dormitory]').change(function ()
{
    document.getElementById('idDepartmental').disabled = true;
    document.getElementById('idDormitory').disabled = false;
});

$('input[value=departmental]').change(function ()
{
    document.getElementById('idDormitory').disabled = true;
    document.getElementById('idDepartmental').disabled = false;
});

$('input[value=from-university]').change(function ()
{
    document.getElementById('idDormitory').disabled = true;
    document.getElementById('idDepartmental').disabled = true;
});
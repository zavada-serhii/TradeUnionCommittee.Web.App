$('#editable-select-LevelEducation').editableSelect();
$('#editable-select-Study').editableSelect();
$('#editable-select-ScientifickDegree').editableSelect();
$('#editable-select-ScientifickTitle').editableSelect();

//------------------------------------------------------------------------------------------------------------------------------------------

$(function () {
    $("#mobile-phone").mask("+38(099)999-99-99");
    $("#Mechnikov-code").mask("9-999999-999999");
    $("#ident-code").mask("9999999999");
    $("#year").mask("9999");
});

//------------------------------------------------------------------------------------------------------------------------------------------

$(document).ready(function () {
    document.getElementById("dormitory").checked = true;
    document.getElementById("idDepartmental").disabled = true;
    document.getElementById("idDormitory").disabled = false;

    document.getElementById("employeeBirthDate").checked = true;
    document.getElementById("employeeHobby").checked = true;
});

//------------------------------------------------------------------------------------------------------------------------------------------

$('input[value=dormitory]').change(function () {
    document.getElementById('idDepartmental').disabled = true;
    document.getElementById('idDormitory').disabled = false;
});

$('input[value=departmental]').change(function () {
    document.getElementById('idDormitory').disabled = true;
    document.getElementById('idDepartmental').disabled = false;
});

$('input[value=from-university]').change(function () {
    document.getElementById('idDormitory').disabled = true;
    document.getElementById('idDepartmental').disabled = true;
});
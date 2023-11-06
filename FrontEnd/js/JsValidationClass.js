function check(a)
{
    if (a.trim().length === 0)
    {
        return false
    }
    return true
}
function check_class(name)
{
    if (check(name))
    {
        return true;
    }
    else
    {
        $("#error_name").show().text("Vui lòng nhập tên lớp");
        return false;
    }
}

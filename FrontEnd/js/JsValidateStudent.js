function check_null(a)
{
    if (a.trim().length === 0)
    {
        return false
    }
    return true
}
function check_email(a)
{
    const emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if (emailRegex.test(a)) {
        return true
      } else {
        return false
      }
}
function check_number(a)
{
    if (!isNaN(a)) {
        return true
      } else {
        return false
      }
}
function check_student(id,name,address,country,date,email,phone)
{
    if (check_null(name) && check_null(id) && check_null(address) && check_null(country) 
    && check_null(date) && check_null(email) && check_email(email) && check_null(phone) 
    && check_number(phone))
    {
        return true;
    }
    else
    {
        if(!check_null(id))
        {
            $("#error_id").show().text("Vui lòng nhập mã sinh viên.");
        }
        else
        {
            $("#error_id").hide();
        }
        if(!check_null(name))
        {
        $("#error_name").show().text("Vui lòng nhập tên sinh viên.");
        }
        else
        {
            $("#error_name").hide();
        }
        if(!check_null(address))
        {
            $("#error_address").show().text("Vui lòng nhập địa chỉ.");
        }
        else
        {
            $("#error_address").hide();
        }
        if(!check_null(country))
        {
            $("#error_country").show().text("Vui lòng nhập quê quán.");
        }
        else
        {
            $("#error_country").hide();
        }
        if(!check_null(date))
        {
            $("#error_date").show().text("Vui lòng nhập ngày sinh.");
        }
        else
        {
            $("#error_date").hide();
        }
        if(!check_null(email))
        {
            $("#error_email").show().text("Vui lòng nhập email.");
        }
        else if(!check_email(email))
        {
            $("#error_email").show().text("Không đúng định dạng email. Vui lòng nhập lại email.");
        }
        else
        {
            $("#error_email").hide();
        }
        if(!check_null(phone))
        {
            $("#error_phone").show().text("Vui lòng nhập số điện thoại.");
        }
        else if(!check_number(phone))
        {
            $("#error_phone").show().text("Số điện thoại không thể có chữ. Vui lòng nhập lại");
        }
        else
        {
            $("#error_phone").hide();
        }
        return false;
    }
}
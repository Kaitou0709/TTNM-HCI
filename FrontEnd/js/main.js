
function validate() {
  var username = document.getElementById("username").value;
  var password = document.getElementById("password-field").value;
  if (username == "admin" && password == "admin") {
      window.location = "Class/Class.html";
      return true;
  }
  else if (username == "" || password == "") {
    var nullModal = new bootstrap.Modal(document.getElementById("NullModal"));
    nullModal.show();
    setTimeout(function() {
      nullModal.hide();
    }, 1000);
  }
  else {
    fetch(`https://localhost:7257/api/Students/${username}`)
    .then((data) => {
      if (!data.ok) {
        var falseModal = new bootstrap.Modal(document.getElementById("falseModal"));
    falseModal.show();
    setTimeout(function() {
      falseModal.hide();
    }, 1000);
      } else return data.json();
    })
    .then((response) => {if (password == "1")
  {
    sessionStorage.setItem('SV', `${username}`);
    window.location = "Page_use/ReSubject.html";
  }
else{
  var falseModal = new bootstrap.Modal(document.getElementById("falseModal"));
    falseModal.show();
    setTimeout(function() {
      falseModal.hide();
    }, 1000);
}})
}
}
/*  PHẦN NỘI DUNG KHÔI PHỤC MẬT KHẨU   */

/* =========================================== */
/* =========================================== */
function RegexEmail(emailInputBox) {
  var emailStr = document.getElementById(emailInputBox).value;
  var emailRegexStr = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
  var isvalid = emailRegexStr.test(emailStr);
  if (emailStr == "") {
    var nullModal = new bootstrap.Modal(document.getElementById("NullModal"));
    nullModal.show();
    setTimeout(function() {
      nullModal.hide();
    }, 1000);
  }
  else if (!isvalid) {
    var falseModal = new bootstrap.Modal(document.getElementById("falseModal"));
    falseModal.show();
    setTimeout(function() {
      falseModal.hide();
    }, 1000);
    emailInputBox.focus;
  }
  else {
    var successModal = new bootstrap.Modal(document.getElementById("successModel"));
    successModal.show();
    setTimeout(function() {
      successModal.hide();
      window.location = "index.html";
    }, 2000);
  }
}

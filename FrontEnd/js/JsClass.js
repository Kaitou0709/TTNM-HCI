const GetAll = document.querySelector('#table tbody')
// function getAll()
// {
//     fetch('https://localhost:7257/api/Grades')
//     .then(data => data.json())
//     .then(response =>  display(response));
// }
// function display(a)
// {   
//     let all = '';
//     a.forEach(a => {
//         let Element = ` 
//     <tr>
//         <td>${a.name}</td>
//         <td>${a.nameTeacher}</td>
//         <td>${a.nameFaculty}</td>
//         <td>
//             <a href="Class_edit.html?id=${a.idGrade}"><i class="fa-solid fa-pen-to-square"></i></a>
//         </td>
//         <td>
//             <button class="btn btn-danger btn-sm" onclick="ConfirmDelete(${a.idGrade})"><i class="fa-solid fa-trash" id="delete"></i></button>
//         </td>
//     </tr> 
//     `;
//     all = all + Element;
//     });
//     GetAll.innerHTML = all;
// }
let currentPage = 1;
let total = 0
let number_page
fetch('https://localhost:7257/api/Grades')
    .then(data => data.json())
    .then(response =>  {localStorage.setItem("dataLength", response.length)});
total =  localStorage.getItem("dataLength");
function checkCookie() {
  var cookies = document.cookie.split(';');
  for (var i = 0; i < cookies.length; i++) {
    var cookie = cookies[i].trim();
    console.log(cookie)
    if (cookie == ("success=1")) 
    {
      total = parseInt(total) + 1
      console.log(total)
      var successModal = new bootstrap.Modal(document.getElementById("addSuccessModal"));
      successModal.show();
      setTimeout(function() {
        successModal.hide();
      }, 1000);
      document.cookie = "success=0";
    }
    else if (cookie == ("success=2"))
    {
      var successModal = new bootstrap.Modal(document.getElementById("editSuccessModal"));
      successModal.show();
      setTimeout(function() {
        successModal.hide();
      }, 1000);
      document.cookie = "success=0";
    }
  }
}
checkCookie()
console.log(total)
function getPage()
{
  number_page = Math.ceil(total / 4 );
}
getPage()
console.log(number_page)
function getAll(p)
{
    fetch(`https://localhost:7257/api/Grades/page/${p}`)
    .then(data => data.json())
    .then(response =>  display(response));
}
function display(a)
{   
    let all = '';
    a.forEach(a => {
        let Element = ` 
    <tr>
        <td>${a.name}</td>
        <td>${a.nameTeacher}</td>
        <td>${a.nameFaculty}</td>
        <td>
            <a href="Class_edit.html?id=${a.idGrade}"><i class="fa-solid fa-pen-to-square"></i></a>
        </td>
        <td>
            <button class="btn btn-danger btn-sm" onclick="ConfirmDelete(${a.idGrade})"><i class="fa-solid fa-trash" id="delete"></i></button>
        </td>
    </tr> 
    `;
    all = all + Element;
    });
    GetAll.innerHTML = all;
}
var page = document.getElementById('page')
function Delete(id)
{
    fetch(`https://localhost:7257/api/Grades/${id}`,
    {
        method: 'DELETE' ,
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`Lỗi HTTP! Trạng thái: ${response.status}`);
        }  
        return response.text();
    })
    .then(data => {
        if(data == "Xoa thanh cong")
        {
            var successModal = new bootstrap.Modal(document.getElementById("deleteSuccessModal"));
      successModal.show();
      setTimeout(function() {
        successModal.hide();
      }, 1000);
        total = total - 1
        localStorage.setItem("dataLength", total)
        getPage()
        if(currentPage > number_page)
        {
          currentPage = number_page
        }
        page.innerHTML = currentPage
        getAll(currentPage)
        update_button()
        }
        else
        {
            var successModal = new bootstrap.Modal(document.getElementById("deleteFalseModal"));
            successModal.show();
            setTimeout(function() {
              successModal.hide();
            }, 1000);
        }
    });;
}
function ConfirmDelete(id)
{
        a = id
        var modal = new bootstrap.Modal(document.getElementById("confirmDeleteModal"));
        modal.show();
        document.getElementById("confirmDeleteButton").addEventListener("click", function() {
        var modal = bootstrap.Modal.getInstance(document.getElementById("confirmDeleteModal"));
        modal.hide()
        Delete(a)
      })
}
getAll(1);
const previousButton = document.getElementById('previousPage');
const nextButton = document.getElementById('nextPage');
function update_button()
{
  if(currentPage == 1)
  {
  previousButton.classList.add('disabled');
  nextButton.classList.remove('disabled');
  }
  else if (currentPage === number_page) {
    previousButton.classList.remove('disabled');
    nextButton.classList.add('disabled');
  }
  else
  {
    previousButton.classList.remove('disabled');
    nextButton.classList.remove('disabled');
  }
}
const pageLink = document.querySelector('.page-link');
console.log(pageLink)
document.getElementById("previousPage").addEventListener("click", () => {
  if (currentPage > 1) {
    event.preventDefault();
    currentPage--;
    page.innerHTML = currentPage
    getAll(currentPage);
    update_button();
    pageLink.blur();
  }
});
document.getElementById("nextPage").addEventListener("click", () => {
  event.preventDefault();
  currentPage++;
  page.innerHTML = currentPage
  getAll(currentPage);
  update_button()
  nextButton.blur();
});

// Ngăn chặn việc focus bằng chuột
page.addEventListener("mousedown", function (event) {
  event.preventDefault();
});

// Ngăn chặn việc focus bằng bàn phím
page.addEventListener("keydown", function (event) {
  if (event.key === "Tab") {
    event.preventDefault();
  }
});



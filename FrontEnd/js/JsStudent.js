const GetAll = document.querySelector('#table tbody')
// function getAll()
// {
//     fetch('https://localhost:7257/api/Students')
//     .then(data => data.json())
//     .then(response =>  display(response));
// }
let currentPage = 1;
let total = 0
let number_page
fetch('https://localhost:7257/api/Students')
    .then(data => data.json())
    .then(response =>  {localStorage.setItem("dataLength_student", response.length);
  console.log(response)});
total = localStorage.getItem("dataLength_student");
function checkCookie() {
  var cookies = document.cookie.split(';');
  for (var i = 0; i < cookies.length; i++) {
    var cookie = cookies[i].trim();
    console.log(cookie)
    if (cookie == ("success_student=1")) 
    {
      total = parseInt(total) + 1
      
      var successModal = new bootstrap.Modal(document.getElementById("addSuccessModal"));
      successModal.show();
      setTimeout(function() {
        successModal.hide();
      }, 1000);
      document.cookie = "success_student=0";
    }
    else if (cookie == ("success_student=2"))
    {
      var successModal = new bootstrap.Modal(document.getElementById("editSuccessModal"));
      successModal.show();
      setTimeout(function() {
        successModal.hide();
      }, 1000);
      document.cookie = "success_student=0";
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
    fetch(`https://localhost:7257/api/Students/page/${p}`)
    .then(data => data.json())
    .then(response =>  display(response));
}
function display(a)
{   
    let all = '';
    a.forEach(a => {
        let Element = ` 
    <tr>
        <th scope="row">${a.idStudent}</th>
        <td>${a.name}</td>
        <td>${a.gender}</td>
        <td>${a.address}</td>
        <td>${a.country}</td>
        <td>${a.birthDay.toString().split('T')[0]}</td>
        <td>${a.email}</td>
        <td>${a.phone}</td>
        <td>${a.nameGrade}</td>
        <td>
            <a href="Student_Edit.html?id=${a.idStudent}"><i class="fa-solid fa-pen-to-square"></i></a>
        </td>
        <td>
            <button class="btn btn-danger btn-sm" onclick="ConfirmDelete(${a.idStudent})"><i class="fa-solid fa-trash" id="delete"></i></button>
        </td>
    </tr> 
    `;
    all = all + Element;
    });
    GetAll.innerHTML = all;
}
function Delete(id)
{
    fetch(`https://localhost:7257/api/Students/${id}`,
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
      localStorage.setItem("dataLength_student", total)
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
            console.log(data);
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
getAll(currentPage);
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
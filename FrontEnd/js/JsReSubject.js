const student = document.querySelector("#basic-usage");
let id = 0;
let c = []
function getStudent() {
  fetch("https://localhost:7257/api/Students")
    .then((data) => data.json())
    .then((response) => displayStudent(response));
}
function displayStudent(a) {
  let all = "";
  a.forEach((a) => {
    let Element = ` 
          <option value="${a.idStudent}">${a.idStudent} - ${a.name}</option>
          `;
    all = all + Element;
  });
  student.innerHTML = all;
}
const semester = document.querySelector("#Semester");
function getSemester() {
  return fetch("https://localhost:7257/api/Semesters")
    .then((data) => data.json())
    .then((response) => displaySemester(response));
}
function displaySemester(a) {
  let all = "";
  a.forEach((a) => {
    let Element = ` 
          <option value="${a.idSemester}">${a.semester}</option>
          `;
    all = all + Element;
  });
  semester.innerHTML = all;
}
getSemester();
getStudent();
const btnSearch = document.querySelector("#search");
function getAllSubjectSemester(idSemester, idStudent) {
  fetch(
    `https://localhost:7257/api/SemesterSubject/SeachingSemester/${idSemester}/${idStudent}`)
    .then((data) => data.json())
    .then((response) =>
      display_SubjectSemester(response, idSemester, idStudent)
    );
}
function display_SubjectSemester(a, idSemester, idStudent) {
  Subject = document.querySelector("#table tbody");
  let all = "";
  a.forEach((a) => {
    let Element = `<tr>
        <td onclick="getClass(${a.idSubject},${idSemester},${idStudent},this)" class="pointer-cursor"><i id="icon_${a.idSubject}" class="fas fa-check hidden"></i>${a.subject}</td>
      </tr>`;
    all = all + Element;
  });
  Subject.innerHTML = all;
  checkSubjectLearn(idSemester, idStudent);
}
function checkSubjectLearn(idSemester, idStudent) {
  fetch(
    `https://localhost:7257/api/ReSubject/SeachingSubject/${idSemester}/${idStudent}`
  )
    .then((data) => {
      if (!data.ok) {
        throw new Error(`Lỗi HTTP! Trạng thái: ${data.status}`);
      } else return data.json();
    })
    .then((response) => {
      response.forEach((a) => {
        displayCheck(a.idSubject);
      });
    });
}
function displayCheck(idSubject) {
  icon = document.getElementById(`icon_${idSubject}`);
  icon.classList.toggle("hidden");
}
// function ClassSubject(idSemester,)
btnSearch.addEventListener("click", function () {
  getAllSubjectSemester(
    document.getElementById("Semester").value,
    document.getElementById("basic-usage").value
  );
  body = document.querySelector("#table_class tbody");
  body.innerHTML = "";
});
function getClass(idSubject, idSemester, idStudent, cell) {
  var selectedCells = document.querySelectorAll(".selected-cell");
  selectedCells.forEach(function (selectedCell) {
    selectedCell.classList.remove("selected-cell");
  });
  cell.classList.add("selected-cell");
  fetch(
    `https://localhost:7257/api/SemesterSubject/SeachingSubject/${idSubject}/${idSemester}`
  )
    .then((data) => data.json())
    .then((response) =>
      display_class(response, idSemester, idSubject, idStudent)
    );
}
function display_class(a, idSemester, idSubject, idStudent) {
  body = document.querySelector("#table_class tbody");
  let all = "";

  a.forEach((a) => {
    if (a.listSchedules.length > 0) {
      let Element = "";
      let e = ` 
      <tr>
      <td colspan="4" class="title_class" id ="Conflicting_${a.idClass}"><span id="name_${a.idClass}">Tên lớp học: ${a.nameClass}</span>
      <input type="checkbox" class="select-checkbox re" id="${a.idClass}"</th>
      </td>
      <tr>
      <th scope="col">Ngày học</th>
      <th scope="col">Tiết học</th>
    <th scope="col">Phòng học</th>
    <th scope="col">Giáo viên</th>
    </tr>
    <tr>
        
        <td>${
          a.semesterDateStart.toString().split("T")[0]
        } <i class="fa-solid fa-arrow-right"></i> ${
        a.semesterDateEnd.toString().split("T")[0]
      }</td>
        <td>${a.listSchedules[0].day}, ${
        a.listSchedules[0].name_Start
      } <i class="fa-solid fa-arrow-right"></i> ${a.listSchedules[0].name_End}
        <br>( ${
          a.listSchedules[0].startTime
        } <i class="fa-solid fa-arrow-right"></i> ${
        a.listSchedules[0].endTime
      } )</td>
      <td>330A5</td>
      <td>${a.nameTeacher}</td>
    </tr> 
    `;
      Element = Element + e;
      for (i = 1; i < a.listSchedules.length; i++) {
        e = `
    <tr>
        <td></td>
        <td>${a.listSchedules[i].day}, ${a.listSchedules[i].name_Start} <i class="fa-solid fa-arrow-right"></i> ${a.listSchedules[i].name_End}
        <br>( ${a.listSchedules[i].startTime} <i class="fa-solid fa-arrow-right"></i> ${a.listSchedules[i].endTime} )</td>
        <td>340A3</td>
        <td></td>
    </tr> 
            `;
        Element = Element + e;
      }
      all = all + Element;
    }
  });
  body.innerHTML = all;
  checkClassLesson(idSemester, idSubject, idStudent);
  fetch(
    `https://localhost:7257/api/ReSubject/SeachingTimeClass/${idSemester}/${idSubject}/${idStudent}`
  )
    .then((data) => {
      if (!data.ok) {
        throw new Error(`Lỗi HTTP! Trạng thái: ${data.status}`);
      } else return data.json();
    })
    .then((response) => {
      let c_check = []
      response.forEach((a) => {
        c_check.push(a.id)
        // console.log(a.id)
        displayCheckConflicting(a.id);
      })
    });
    checkboxes = document.querySelectorAll(".select-checkbox");
  checkboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", () => {
      // console.log(id);
      if (checkbox.checked) {
        if (id == 0) {
          //console.log(document.getElementById(`name_${checkbox.getAttribute("id")}`).textContent.split(":")[1].trim())
          const body = {
            idSubject: checkbox.getAttribute("id"),
            idStudent: idStudent,
          };
          fetch(`https://localhost:7257/api/ReSubject`, {
            method: "POST",
            body: JSON.stringify(body),
            headers: { "content-type": "application/json" },
          })
            .then((response) => {
              if (!response.ok) {
                throw new Error(`Lỗi HTTP! Trạng thái: ${response.status}`);
              } else return response.text();
            })
            .then((data) => {
              if (data ==  "Trung mon hoc")
              {
                    var false_re = document.getElementById('false_re');
                    false_re.innerHTML = `Lớp ${document.getElementById(`name_${checkbox.getAttribute("id")}`).textContent.split(":")[1].trim()} đã bị trùng lịch học. Vui lòng đăng ký lớp khác.`;
                    check = document.getElementById(checkbox.id)
                    check.checked = false
                    $(function() {
                      $('#FalseModal').modal('show');
                    });
                    setTimeout(function() {
                      $('#FalseModal').modal('hide');
                    }, 1000);
              }
              else if(data == "Dang ky mon thanh cong") {
                fetch(
                  `https://localhost:7257/api/ReSubject/SeachingSubject/${idSemester}/${idSubject}/${idStudent}`
                )
                  .then((data) => data.json())
                  .then((response) => {
                    id = response.idReSubject;
                  });
                icon = document.getElementById(`icon_${idSubject}`);
                icon.classList.toggle("hidden");
                var re_class = document.getElementById('re_class');
                re_class.innerHTML = `Bạn đã đăng ký thành công lớp ${document.getElementById(`name_${checkbox.getAttribute("id")}`).textContent.split(":")[1].trim()}`;
                $(function() {
                  $('#addSuccessModal').modal('show');
                });
                setTimeout(function() {
                  $('#addSuccessModal').modal('hide');
                }, 1000);
              } 
              else {
                alert(data);
              }
            });
        } else {
          const body = {
            idReSubject: id,
            idSubject: checkbox.getAttribute("id"),
            idStudent: idStudent,
          };
          fetch(`https://localhost:7257/api/ReSubject/${id}`, {
            method: "PUT",
            body: JSON.stringify(body),
            headers: { "content-type": "application/json" },
          })
            .then((response) => {
              if (!response.ok) {
                throw new Error(`Lỗi HTTP! Trạng thái: ${response.status}`);
              } else return response.text();
            })
            .then((data) => {
              if (data ==  "Trung mon hoc")
              {
                    var false_re = document.getElementById('false_re');
                    false_re.innerHTML = `Lớp ${document.getElementById(`name_${checkbox.getAttribute("id")}`).textContent.split(":")[1].trim()} đã bị trùng lịch học. Vui lòng đăng ký lớp khác.`;
                    check = document.getElementById(checkbox.id)
                    check.checked = false
                    $(function() {
                      $('#FalseModal').modal('show');
                    });
                    setTimeout(function() {
                      $('#FalseModal').modal('hide');
                    }, 1000);
              }
              else if (data == "Sua thanh cong") {
                checkboxes.forEach((otherCheckbox) => {
                  if (otherCheckbox !== checkbox) {
                    otherCheckbox.checked = false;
                  }
                });
                var edit_class = document.getElementById('edit_class');
                edit_class.innerHTML = `Bạn đã chuyển thành công lớp thành lớp ${document.getElementById(`name_${checkbox.getAttribute("id")}`).textContent.split(":")[1].trim()}`;
                $(function() {
                  $('#editSuccessModal').modal('show');
                });
                setTimeout(function() {
                  $('#editSuccessModal').modal('hide');
                }, 1000);
              } else {
                alert(data);
              }
            });
        }
      } 
      else {
        var del_class = document.getElementById('del_class');
        del_class.innerHTML = `Bạn có chắc chắn muốn hủy lớp ${document.getElementById(`name_${checkbox.getAttribute("id")}`).textContent.split(":")[1].trim()}`;
        check = document.getElementById(checkbox.id)
        check.checked = true
        $('#confirmDeleteModal').modal('show');
        document.getElementById("confirmDeleteButton").addEventListener("click", function() {
          var modal = bootstrap.Modal.getInstance(document.getElementById("confirmDeleteModal"));
          modal.hide()
          Delete(`icon_${idSubject}`, checkbox.id)
        })
      }
    });
  });
}
function checkClassLesson(idSemester, idSubject, idStudent) {
  id = 0;
  fetch(
    `https://localhost:7257/api/ReSubject/SeachingSubject/${idSemester}/${idSubject}/${idStudent}`
  )
    .then((data) => {
      if (!data.ok) {
        throw new Error(`Lỗi HTTP! Trạng thái: ${data.status}`);
      } else return data.json();
    })
    .then((response) => {
      id = response.idReSubject;
      document.getElementById(`${response.idClass}`).checked = true;
    });
}
function Delete(a,check)
{
  fetch(`https://localhost:7257/api/ReSubject/${id}`, {
          method: "DELETE",
          body: JSON.stringify(body),
          headers: { "content-type": "application/json" },
        })
          .then((response) => {
            if (!response.ok) {
              throw new Error(`Lỗi HTTP! Trạng thái: ${response.status}`);
            } else return response.text();
          })
          .then((data) => {
            if (data == "Xoa thanh cong") {
              console.log(check)
              checkbox = document.getElementById(check)
              checkbox.checked = false
              icon = document.getElementById(a);
              icon.classList.toggle("hidden");
              $('#deleteSuccessModal').modal('show');
              setTimeout(function() {
                $('#deleteSuccessModal').modal('hide');
              }, 1000);
              id = 0;
            } else {
              alert(data);
            }
          });
}
function displayCheckConflicting(id)
{
  Conflicting = document.getElementById(`Conflicting_${id}`)
  Conflicting.classList.add('Conflicting_background');
}
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms'
@Component({
  selector: 'app-create-new-project',
  templateUrl: './create-new-project.component.html',
  styleUrls: ['./create-new-project.component.css']
})
export class CreateNewProjectComponent implements OnInit {
  projectName?: string;
  createProjectForm?: FormGroup;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  createProject() {
    this.http.post(`https://localhost:7263/api/Projects`, { name: this.projectName })
      .subscribe((response: any) => {
        console.log(response);
      });
  }
}
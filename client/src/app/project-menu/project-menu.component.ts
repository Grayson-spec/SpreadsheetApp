import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-menu',
  templateUrl: './project-menu.component.html',
  styleUrls: ['./project-menu.component.css']
})
export class ProjectMenuComponent implements OnInit {
  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  openProject() {
    this.router.navigate(['/open-project']);
  }

  createNewProject() {
    this.router.navigate(['/create-new-project']);
  }
}
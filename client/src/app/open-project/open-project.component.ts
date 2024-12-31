import { Component, ElementRef, Renderer2 } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-open-project',
  templateUrl: './open-project.component.html',
  styleUrls: ['./open-project.component.css']
})
export class OpenProjectComponent {
  constructor(private http: HttpClient, private elementRef: ElementRef, private renderer: Renderer2) { }

  searchProject() {
    const projectName = this.elementRef.nativeElement.querySelector('#projectName').value;
    this.http.get(`https://localhost:7263/api/Projects?name=${projectName}`)
      .subscribe((response: any) => {
        const projects = this.elementRef.nativeElement.querySelector('#projects');
        projects.innerHTML = '';
        response.forEach((project: string) => {
          const listItem = this.renderer.createElement('li');
          listItem.textContent = project;
          this.renderer.appendChild(projects, listItem);
        });
      });
  }
}
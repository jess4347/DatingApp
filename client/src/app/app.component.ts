import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpContext } from '@angular/common/http';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'client';
  http = inject(HttpClient);
  users: any;
  ngOnInit() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (response) => {
        this.users = response;
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        console.log('completed');
      },
    });
    console.log(this.users);
  }
}

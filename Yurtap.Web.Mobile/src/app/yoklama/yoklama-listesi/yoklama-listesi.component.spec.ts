import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { YoklamaListesiComponent } from './yoklama-listesi.component';

describe('YoklamalarComponent', () => {
  let component: YoklamaListesiComponent;
  let fixture: ComponentFixture<YoklamaListesiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YoklamaListesiComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YoklamaListesiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ProjetoSQLCLR
{
    public class SMTP
    {
        private readonly List<string> Destinatarios;
        private readonly ContaEmail Conta;
        private readonly string Assunto;
        private readonly List<string> DestinatariosCopia;

        public SMTP(ContaEmail conta, List<string> destinatarios,
                    string assunto, List<string> destinatariosCopia = null)
        {
            Conta = conta;
            Destinatarios = destinatarios;
            Assunto = assunto;
            DestinatariosCopia = destinatariosCopia;
        }

        public SMTP(ContaEmail conta, string destinatario,
                   string assunto, List<string> destinatariosCopia = null)
        {
            Conta = conta;
            Destinatarios = new List<string>();
            Destinatarios.Add(destinatario);
            Assunto = assunto;
            DestinatariosCopia = destinatariosCopia;
        }

        public void Enviar(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new Exception("O corpo do email não pode estar vazio");

            using (var email = new MailMessage())
            {
                using (var smtp = new SmtpClient())
                {
                    //Remetente
                    email.From = new MailAddress(Conta.Remetente);

                    //Destinatários
                    foreach (var item in Destinatarios)
                    {
                        email.To.Add(new MailAddress(item));
                    }

                    //Destinatários em cópia (apenas se houver)
                    if (DestinatariosCopia != null)
                    {
                        foreach (var item in DestinatariosCopia)
                        {
                            email.CC.Add(new MailAddress(item));
                        }
                    }

                    //Assunto
                    email.Subject = Assunto;

                    //HTML
                    email.Body = html;
                    email.IsBodyHtml = true;

                    //Codificação UTF (Padrão)
                    email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                    email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                    //Prioridade
                    email.Priority = MailPriority.Normal;

                    //SMTP
                    smtp.Host = Conta.Servidor;
                    smtp.Port = Conta.Porta;
                    smtp.Credentials = new NetworkCredential(Conta.Usuario, Conta.Senha);
                    smtp.EnableSsl = Conta.SSL;
                    smtp.Send(email);
                }
            }
        }
    }
}
